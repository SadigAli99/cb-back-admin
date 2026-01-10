using CB.Application.DTOs.PercentCorridorCategory;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.PercentCorridorCategory;
using CB.Shared.Helpers;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class PercentCorridorCategoryController : ControllerBase
    {
        private readonly IPercentCorridorCategoryService _percentCorridorCategoryService;
        private readonly PercentCorridorCategoryCreateValidator _createValidator;
        private readonly PercentCorridorCategoryEditValidator _editValidator;

        public PercentCorridorCategoryController(
            IPercentCorridorCategoryService percentCorridorCategoryService,
            PercentCorridorCategoryCreateValidator createValidator,
            PercentCorridorCategoryEditValidator editValidator
        )
        {
            _percentCorridorCategoryService = percentCorridorCategoryService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/percentCorridorCategory
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _percentCorridorCategoryService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/percentCorridorCategory/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _percentCorridorCategoryService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Faiz dəhlizi kateqoriyası məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        // POST: /api/percentCorridorCategory
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PercentCorridorCategoryCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            dto.Slugs = SlugHelper.GenerateSlugs(dto.Titles);
            var created = await _percentCorridorCategoryService.CreateAsync(dto);

            if (!created)
            {
                Log.Warning("Faiz dəhlizi kateqoriyası məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dto);
                return BadRequest();
            }

            Log.Information("Faiz dəhlizi kateqoriyası məlumatı uğurla əlavə olundu : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/percentCorridorCategory/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] PercentCorridorCategoryEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            dto.Slugs = SlugHelper.GenerateSlugs(dto.Titles);
            var updated = await _percentCorridorCategoryService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Warning("Faiz dəhlizi kateqoriyası məlumatı yenilənməsi uğursuz oldu : {@Dto}", dto);
                return NotFound();
            }
            Log.Information("Faiz dəhlizi kateqoriyası məlumatı yenilənməsi uğurludur : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });

        }

        // DELETE: /api/percentCorridorCategory/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _percentCorridorCategoryService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Faiz dəhlizi kateqoriyası məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Faiz dəhlizi kateqoriyası məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }
    }
}
