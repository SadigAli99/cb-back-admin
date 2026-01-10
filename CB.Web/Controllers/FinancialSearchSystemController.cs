using CB.Application.DTOs.FinancialSearchSystem;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.FinancialSearchSystem;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FinancialSearchSystemController : ControllerBase
    {
        private readonly IFinancialSearchSystemService _financialSearchSystemService;
        private readonly FinancialSearchSystemCreateValidator _createValidator;
        private readonly FinancialSearchSystemEditValidator _editValidator;

        public FinancialSearchSystemController(
            IFinancialSearchSystemService financialSearchSystemService,
            FinancialSearchSystemCreateValidator createValidator,
            FinancialSearchSystemEditValidator editValidator
        )
        {
            _financialSearchSystemService = financialSearchSystemService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<FinancialSearchSystemGetDTO> data = await _financialSearchSystemService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            FinancialSearchSystemGetDTO? data = await _financialSearchSystemService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Maliyyə məhsul və xidmətlərinin məlumat-axtarış sistemi məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] FinancialSearchSystemCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _financialSearchSystemService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Maliyyə məhsul və xidmətlərinin məlumat-axtarış sistemi məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Maliyyə məhsul və xidmətlərinin məlumat-axtarış sistemi məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] FinancialSearchSystemEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _financialSearchSystemService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Maliyyə məhsul və xidmətlərinin məlumat-axtarış sistemi məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Maliyyə məhsul və xidmətlərinin məlumat-axtarış sistemi məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _financialSearchSystemService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Maliyyə məhsul və xidmətlərinin məlumat-axtarış sistemi məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Maliyyə məhsul və xidmətlərinin məlumat-axtarış sistemi məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

        [HttpDelete("{financialSearchSystemId}/images/{imageId}")]
        public async Task<IActionResult> DeleteImage(int financialSearchSystemId, int imageId)
        {
            var result = await _financialSearchSystemService.DeleteImageAsync(financialSearchSystemId, imageId);
            if (!result)
            {
                Log.Warning("Maliyyə məhsul və xidmətlərinin məlumat-axtarış sistemi şəkli silinə bilmədi : FinancialSearchSystemId = {@FinancialSearchSystemId}, ImageId = {@ImageId}", financialSearchSystemId, imageId);
                return NotFound(new { Message = "Şəkil tapılmadı və ya silinə bilmədi." });
            }

            Log.Warning("Maliyyə məhsul və xidmətlərinin məlumat-axtarış sistemi şəkli uğurla silindi : FinancialSearchSystemId = {@FinancialSearchSystemId}, ImageId = {@ImageId}", financialSearchSystemId, imageId);
            return Ok(new { Message = "Şəkil uğurla silindi." });
        }

    }
}
