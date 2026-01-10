using CB.Application.DTOs.PaymentSystemControlService;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.PaymentSystemControlService;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaymentSystemControlServiceController : ControllerBase
    {
        private readonly IPaymentSystemControlServiceService _paymentSystemControlServiceService;
        private readonly PaymentSystemControlServiceCreateValidator _createValidator;
        private readonly PaymentSystemControlServiceEditValidator _editValidator;

        public PaymentSystemControlServiceController(
            IPaymentSystemControlServiceService paymentSystemControlServiceService,
            PaymentSystemControlServiceCreateValidator createValidator,
            PaymentSystemControlServiceEditValidator editValidator
        )
        {
            _paymentSystemControlServiceService = paymentSystemControlServiceService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int id)
        {
            List<PaymentSystemControlServiceGetDTO> data = await _paymentSystemControlServiceService.GetAllAsync(id);
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            PaymentSystemControlServiceGetDTO? data = await _paymentSystemControlServiceService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Ödəmə sistemləri xidmət mərkəzi məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] PaymentSystemControlServiceCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _paymentSystemControlServiceService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Ödəmə sistemləri xidmət mərkəzi məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Ödəmə sistemləri xidmət mərkəzi məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] PaymentSystemControlServiceEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _paymentSystemControlServiceService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Ödəmə sistemləri xidmət mərkəzi məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Ödəmə sistemləri xidmət mərkəzi məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _paymentSystemControlServiceService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Ödəmə sistemləri xidmət mərkəzi məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Ödəmə sistemləri xidmət mərkəzi məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }

    }
}
