using CB.Application.DTOs.FinancialLiteracyPortal;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.FinancialLiteracyPortal;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FinancialLiteracyPortalController : ControllerBase
    {
        private readonly IFinancialLiteracyPortalService _financialLiteracyPortalService;
        private readonly FinancialLiteracyPortalCreateValidator _createValidator;
        private readonly FinancialLiteracyPortalEditValidator _editValidator;

        public FinancialLiteracyPortalController(
            IFinancialLiteracyPortalService financialLiteracyPortalService,
            FinancialLiteracyPortalCreateValidator createValidator,
            FinancialLiteracyPortalEditValidator editValidator
        )
        {
            _financialLiteracyPortalService = financialLiteracyPortalService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<FinancialLiteracyPortalGetDTO> data = await _financialLiteracyPortalService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            FinancialLiteracyPortalGetDTO? data = await _financialLiteracyPortalService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Maliyyə savadlılığı portal məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] FinancialLiteracyPortalCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _financialLiteracyPortalService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Maliyyə savadlılığı portal məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Maliyyə savadlılığı portal məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] FinancialLiteracyPortalEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _financialLiteracyPortalService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Maliyyə savadlılığı portal məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Maliyyə savadlılığı portal məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _financialLiteracyPortalService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Maliyyə savadlılığı portal məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Maliyyə savadlılığı portal məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

        [HttpDelete("{financialLiteracyPortalId}/images/{imageId}")]
        public async Task<IActionResult> DeleteImage(int financialLiteracyPortalId, int imageId)
        {
            var result = await _financialLiteracyPortalService.DeleteImageAsync(financialLiteracyPortalId, imageId);
            if (!result)
            {
                Log.Warning("Maliyyə savadlılığı portal şəkli silinə bilmədi : FinancialLiteracyPortalId = {@FinancialLiteracyPortalId}, ImageId = {@ImageId}", financialLiteracyPortalId, imageId);
                return NotFound(new { Message = "Şəkil tapılmadı və ya silinə bilmədi." });
            }

            Log.Warning("Maliyyə savadlılığı portal şəkli uğurla silindi : FinancialLiteracyPortalId = {@FinancialLiteracyPortalId}, ImageId = {@ImageId}", financialLiteracyPortalId, imageId);
            return Ok(new { Message = "Şəkil uğurla silindi." });
        }

    }
}
