using CB.Application.DTOs.PaymentSystemControl;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.PaymentSystemControl;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaymentSystemControlController : ControllerBase
    {
        private readonly IPaymentSystemControlService _paymentSystemControlService;
        private readonly PaymentSystemControlCreateValidator _createValidator;
        private readonly PaymentSystemControlEditValidator _editValidator;

        public PaymentSystemControlController(
            IPaymentSystemControlService paymentSystemControlService,
            PaymentSystemControlCreateValidator createValidator,
            PaymentSystemControlEditValidator editValidator
        )
        {
            _paymentSystemControlService = paymentSystemControlService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<PaymentSystemControlGetDTO> data = await _paymentSystemControlService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            PaymentSystemControlGetDTO? data = await _paymentSystemControlService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Elan məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PaymentSystemControlCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _paymentSystemControlService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Elan məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Elan məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] PaymentSystemControlEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _paymentSystemControlService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Elan məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Elan məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _paymentSystemControlService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Elan məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Elan məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }

    }
}
