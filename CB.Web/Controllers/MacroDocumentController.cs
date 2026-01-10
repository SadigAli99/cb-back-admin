using CB.Application.DTOs.MacroDocument;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.MacroDocument;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MacroDocumentController : ControllerBase
    {
        private readonly IMacroDocumentService _macroDocumentService;
        private readonly MacroDocumentCreateValidator _createValidator;
        private readonly MacroDocumentEditValidator _editValidator;

        public MacroDocumentController(
            IMacroDocumentService macroDocumentService,
            MacroDocumentCreateValidator createValidator,
            MacroDocumentEditValidator editValidator
        )
        {
            _macroDocumentService = macroDocumentService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<MacroDocumentGetDTO> data = await _macroDocumentService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            MacroDocumentGetDTO? data = await _macroDocumentService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Makro iqtisadi sənəd məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] MacroDocumentCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _macroDocumentService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Makro iqtisadi sənəd məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Makro iqtisadi sənəd məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] MacroDocumentEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _macroDocumentService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Makro iqtisadi sənəd məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Makro iqtisadi sənəd məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _macroDocumentService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Makro iqtisadi sənəd məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Makro iqtisadi sənəd məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
