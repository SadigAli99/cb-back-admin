using CB.Application.DTOs.Translate;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.Translate;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class TranslateController : ControllerBase
    {
        private readonly ITranslateService _translateService;
        private readonly TranslateCreateValidator _createValidator;
        private readonly TranslateEditValidator _editValidator;

        public TranslateController(
            ITranslateService translateService,
            TranslateCreateValidator createValidator,
            TranslateEditValidator editValidator
        )
        {
            _translateService = translateService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/translate
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _translateService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/translate/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _translateService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Tərcümə məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        // POST: /api/translate
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TranslateCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _translateService.CreateAsync(dto);
            if (!created)
            {
                Log.Warning("Tərcümə məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dto);
                return BadRequest();
            }

            Log.Information("Tərcümə məlumatı uğurla əlavə olundu : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/translate/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] TranslateEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });

            var updated = await _translateService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Warning("Tərcümə məlumatı yenilənməsi uğursuz oldu : {@Dto}", dto);
                return NotFound();
            }
            Log.Information("Tərcümə məlumatı yenilənməsi uğurludur : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });

        }

        // DELETE: /api/translate/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _translateService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Tərcümə məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Tərcümə məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }
    }
}
