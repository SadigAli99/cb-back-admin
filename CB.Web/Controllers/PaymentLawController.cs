using CB.Application.DTOs.PaymentLaw;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.PaymentLaw;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaymentLawController : ControllerBase
    {
        private readonly IPaymentLawService _paymentLawService;
        private readonly PaymentLawCreateValidator _createValidator;
        private readonly PaymentLawEditValidator _editValidator;

        public PaymentLawController(
            IPaymentLawService paymentLawService,
            PaymentLawCreateValidator createValidator,
            PaymentLawEditValidator editValidator
        )
        {
            _paymentLawService = paymentLawService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<PaymentLawGetDTO> data = await _paymentLawService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            PaymentLawGetDTO? data = await _paymentLawService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Ödəniş sistemləri qanunlar məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] PaymentLawCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _paymentLawService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Ödəniş sistemləri qanunlar məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Ödəniş sistemləri qanunlar məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] PaymentLawEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _paymentLawService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Ödəniş sistemləri qanunlar məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Ödəniş sistemləri qanunlar məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _paymentLawService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Ödəniş sistemləri qanunlar məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Ödəniş sistemləri qanunlar məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
