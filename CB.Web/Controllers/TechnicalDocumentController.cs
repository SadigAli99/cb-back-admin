using CB.Application.DTOs.TechnicalDocument;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.TechnicalDocument;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TechnicalDocumentController : ControllerBase
    {
        private readonly ITechnicalDocumentService _technicalDocumentService;
        private readonly TechnicalDocumentCreateValidator _createValidator;
        private readonly TechnicalDocumentEditValidator _editValidator;

        public TechnicalDocumentController(
            ITechnicalDocumentService technicalDocumentService,
            TechnicalDocumentCreateValidator createValidator,
            TechnicalDocumentEditValidator editValidator
        )
        {
            _technicalDocumentService = technicalDocumentService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<TechnicalDocumentGetDTO> data = await _technicalDocumentService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            TechnicalDocumentGetDTO? data = await _technicalDocumentService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Texniki sənəd məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] TechnicalDocumentCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _technicalDocumentService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Texniki sənəd məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Texniki sənəd məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] TechnicalDocumentEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _technicalDocumentService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Texniki sənəd məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Texniki sənəd məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _technicalDocumentService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Texniki sənəd məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Texniki sənəd məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
