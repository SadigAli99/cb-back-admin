using CB.Application.DTOs.PaymentSystemStandart;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.PaymentSystemStandart;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaymentSystemStandartController : ControllerBase
    {
        private readonly IPaymentSystemStandartService _paymentSystemStandartService;
        private readonly PaymentSystemStandartCreateValidator _createValidator;
        private readonly PaymentSystemStandartEditValidator _editValidator;

        public PaymentSystemStandartController(
            IPaymentSystemStandartService paymentSystemStandartService,
            PaymentSystemStandartCreateValidator createValidator,
            PaymentSystemStandartEditValidator editValidator
        )
        {
            _paymentSystemStandartService = paymentSystemStandartService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<PaymentSystemStandartGetDTO> data = await _paymentSystemStandartService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            PaymentSystemStandartGetDTO? data = await _paymentSystemStandartService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("ödəniş sistemi standart məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] PaymentSystemStandartCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _paymentSystemStandartService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("ödəniş sistemi standart məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("ödəniş sistemi standart məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] PaymentSystemStandartEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _paymentSystemStandartService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("ödəniş sistemi standart məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("ödəniş sistemi standart məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _paymentSystemStandartService.DeleteAsync(id);

            if (!deleted)
            {
                Log.Warning("ödəniş sistemi standart məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("ödəniş sistemi standart məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });


        }

    }
}
