using CB.Application.DTOs.ControlMeasureCategory;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.ControlMeasureCategory;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class ControlMeasureCategoryController : ControllerBase
    {
        private readonly IControlMeasureCategoryService _controlMeasureCategoryService;
        private readonly ControlMeasureCategoryCreateValidator _createValidator;
        private readonly ControlMeasureCategoryEditValidator _editValidator;

        public ControlMeasureCategoryController(
            IControlMeasureCategoryService controlMeasureCategoryService,
            ControlMeasureCategoryCreateValidator createValidator,
            ControlMeasureCategoryEditValidator editValidator
        )
        {
            _controlMeasureCategoryService = controlMeasureCategoryService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/controlMeasureCategory
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _controlMeasureCategoryService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/controlMeasureCategory/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _controlMeasureCategoryService.GetByIdAsync(id);
            if (data == null)
            {
                Log.Warning("Tətbiq edilmiş nəzarət kateqoriyası məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }

            return Ok(new { status = "success", data });
        }

        // POST: /api/controlMeasureCategory
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ControlMeasureCategoryCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _controlMeasureCategoryService.CreateAsync(dto);

            if (!created)
            {
                Log.Warning("Tətbiq edilmiş nəzarət kateqoriyası məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dto);
                return BadRequest();
            }
            Log.Information("Tətbiq edilmiş nəzarət kateqoriyası məlumatı uğurla əlavə olundu : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/controlMeasureCategory/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] ControlMeasureCategoryEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var updated = await _controlMeasureCategoryService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Warning("Tətbiq edilmiş nəzarət kateqoriyası məlumatı yenilənməsi uğursuz oldu : {@Dto}", dto);
                return NotFound();
            }
            Log.Information("Tətbiq edilmiş nəzarət kateqoriyası məlumatı yenilənməsi uğurludur : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        // DELETE: /api/controlMeasureCategory/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _controlMeasureCategoryService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Tətbiq edilmiş nəzarət kateqoriyası məlumatının silinməsi uğursuzdur : Id = {@Id}", id);

                return NotFound();
            }

            Log.Information("Tətbiq edilmiş nəzarət kateqoriyası məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }
    }
}
