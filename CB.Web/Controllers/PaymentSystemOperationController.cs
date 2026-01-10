using CB.Application.DTOs.PaymentSystemOperation;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.PaymentSystemOperation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaymentSystemOperationController : ControllerBase
    {
        private readonly IPaymentSystemOperationService _paymentSystemOperationService;
        private readonly PaymentSystemOperationCreateValidator _createValidator;
        private readonly PaymentSystemOperationEditValidator _editValidator;

        public PaymentSystemOperationController(
            IPaymentSystemOperationService paymentSystemOperationService,
            PaymentSystemOperationCreateValidator createValidator,
            PaymentSystemOperationEditValidator editValidator
        )
        {
            _paymentSystemOperationService = paymentSystemOperationService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<PaymentSystemOperationGetDTO> data = await _paymentSystemOperationService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            PaymentSystemOperationGetDTO? data = await _paymentSystemOperationService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Ödəmə sistemləri operatorları məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] PaymentSystemOperationCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _paymentSystemOperationService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Ödəmə sistemləri operatorları məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Ödəmə sistemləri operatorları məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] PaymentSystemOperationEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _paymentSystemOperationService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Ödəmə sistemləri operatorları məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Ödəmə sistemləri operatorları məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _paymentSystemOperationService.DeleteAsync(id);

            if (!deleted)
            {
                Log.Warning("Ödəmə sistemləri operatorları məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Ödəmə sistemləri operatorları məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });


        }

    }
}
