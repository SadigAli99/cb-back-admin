using CB.Application.DTOs.PaymentInstitution;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.PaymentInstitution;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaymentInstitutionController : ControllerBase
    {
        private readonly IPaymentInstitutionService _paymentInstitutionService;
        private readonly PaymentInstitutionCreateValidator _createValidator;
        private readonly PaymentInstitutionEditValidator _editValidator;

        public PaymentInstitutionController(
            IPaymentInstitutionService PpymentInstitutionService,
            PaymentInstitutionCreateValidator createValidator,
            PaymentInstitutionEditValidator editValidator
        )
        {
            _paymentInstitutionService = PpymentInstitutionService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<PaymentInstitutionGetDTO> data = await _paymentInstitutionService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            PaymentInstitutionGetDTO? data = await _paymentInstitutionService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Ödəniş təşkilatları məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] PaymentInstitutionCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _paymentInstitutionService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Ödəniş təşkilatları məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Ödəniş təşkilatları məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] PaymentInstitutionEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _paymentInstitutionService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Ödəniş təşkilatları məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Ödəniş təşkilatları məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _paymentInstitutionService.DeleteAsync(id);

            if (!deleted)
            {
                Log.Warning("Ödəniş təşkilatları məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Ödəniş təşkilatları məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });


        }

    }
}
