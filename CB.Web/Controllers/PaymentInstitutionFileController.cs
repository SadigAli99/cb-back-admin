using CB.Application.DTOs.PaymentInstitutionFile;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.PaymentInstitutionFile;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaymentInstitutionFileController : ControllerBase
    {
        private readonly IPaymentInstitutionFileService _paymentInstitutionFileService;
        private readonly PaymentInstitutionFileCreateValidator _createValidator;
        private readonly PaymentInstitutionFileEditValidator _editValidator;

        public PaymentInstitutionFileController(
            IPaymentInstitutionFileService PpymentInstitutionFileService,
            PaymentInstitutionFileCreateValidator createValidator,
            PaymentInstitutionFileEditValidator editValidator
        )
        {
            _paymentInstitutionFileService = PpymentInstitutionFileService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<PaymentInstitutionFileGetDTO> data = await _paymentInstitutionFileService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            PaymentInstitutionFileGetDTO? data = await _paymentInstitutionFileService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Ödəniş təşkilatları fayl məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] PaymentInstitutionFileCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _paymentInstitutionFileService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("PaymentInstitution fayl məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Ödəniş təşkilatları fayl məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] PaymentInstitutionFileEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _paymentInstitutionFileService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Ödəniş təşkilatları fayl məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("PaymentInstitution fayl məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _paymentInstitutionFileService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Ödəniş təşkilatları fayl məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Ödəniş təşkilatları fayl məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
