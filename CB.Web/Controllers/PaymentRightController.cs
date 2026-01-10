using CB.Application.DTOs.PaymentRight;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.PaymentRight;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaymentRightController : ControllerBase
    {
        private readonly IPaymentRightService _paymentRightService;
        private readonly PaymentRightCreateValidator _createValidator;
        private readonly PaymentRightEditValidator _editValidator;

        public PaymentRightController(
            IPaymentRightService paymentRightService,
            PaymentRightCreateValidator createValidator,
            PaymentRightEditValidator editValidator
        )
        {
            _paymentRightService = paymentRightService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<PaymentRightGetDTO> data = await _paymentRightService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            PaymentRightGetDTO? data = await _paymentRightService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Ödəmə sistemləri qaydalar məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] PaymentRightCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _paymentRightService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Ödəmə sistemləri qaydalar məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Ödəmə sistemləri qaydalar məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] PaymentRightEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _paymentRightService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Ödəmə sistemləri qaydalar məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Ödəmə sistemləri qaydalar məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _paymentRightService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Ödəmə sistemləri qaydalar məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Ödəmə sistemləri qaydalar məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
