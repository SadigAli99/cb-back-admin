using CB.Application.DTOs.PaymentSystemStandartFAQ;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.PaymentSystemStandartFAQ;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaymentSystemStandartFAQController : ControllerBase
    {
        private readonly IPaymentSystemStandartFAQService _paymentSystemStandartFAQService;
        private readonly PaymentSystemStandartFAQCreateValidator _createValidator;
        private readonly PaymentSystemStandartFAQEditValidator _editValidator;

        public PaymentSystemStandartFAQController(
            IPaymentSystemStandartFAQService paymentSystemStandartFAQService,
            PaymentSystemStandartFAQCreateValidator createValidator,
            PaymentSystemStandartFAQEditValidator editValidator
        )
        {
            _paymentSystemStandartFAQService = paymentSystemStandartFAQService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int id)
        {
            List<PaymentSystemStandartFAQGetDTO> data = await _paymentSystemStandartFAQService.GetAllAsync(id);
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            PaymentSystemStandartFAQGetDTO? data = await _paymentSystemStandartFAQService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Ödəmə sistemi FAQ məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PaymentSystemStandartFAQCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _paymentSystemStandartFAQService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Ödəmə sistemi FAQ məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Ödəmə sistemi FAQ məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] PaymentSystemStandartFAQEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _paymentSystemStandartFAQService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Ödəmə sistemi FAQ məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Ödəmə sistemi FAQ məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _paymentSystemStandartFAQService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Ödəmə sistemi FAQ məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Ödəmə sistemi FAQ məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }

    }
}
