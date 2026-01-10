using CB.Application.DTOs.PaymentService;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.PaymentService;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaymentServiceController : ControllerBase
    {
        private readonly IPaymentServiceService _paymentServiceService;
        private readonly PaymentServiceCreateValidator _createValidator;
        private readonly PaymentServiceEditValidator _editValidator;

        public PaymentServiceController(
            IPaymentServiceService paymentServiceService,
            PaymentServiceCreateValidator createValidator,
            PaymentServiceEditValidator editValidator
        )
        {
            _paymentServiceService = paymentServiceService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<PaymentServiceGetDTO> data = await _paymentServiceService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            PaymentServiceGetDTO? data = await _paymentServiceService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Ödəniş sistemi məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] PaymentServiceCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _paymentServiceService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Ödəniş sistemi məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Ödəniş sistemi məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] PaymentServiceEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _paymentServiceService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Ödəniş sistemi məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Ödəniş sistemi məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _paymentServiceService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Ödəniş sistemi məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Ödəniş sistemi məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
