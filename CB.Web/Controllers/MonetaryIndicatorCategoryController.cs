using CB.Application.DTOs.MonetaryIndicatorCategory;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.MonetaryIndicatorCategory;
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
    public class MonetaryIndicatorCategoryController : ControllerBase
    {
        private readonly IMonetaryIndicatorCategoryService _monetaryIndicatorCategoryService;
        private readonly MonetaryIndicatorCategoryCreateValidator _createValidator;
        private readonly MonetaryIndicatorCategoryEditValidator _editValidator;

        public MonetaryIndicatorCategoryController(
            IMonetaryIndicatorCategoryService monetaryIndicatorCategoryService,
            MonetaryIndicatorCategoryCreateValidator createValidator,
            MonetaryIndicatorCategoryEditValidator editValidator
        )
        {
            _monetaryIndicatorCategoryService = monetaryIndicatorCategoryService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/monetaryIndicatorCategory
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _monetaryIndicatorCategoryService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/monetaryIndicatorCategory/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _monetaryIndicatorCategoryService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Monetar göstərici kateqoriyası məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        // POST: /api/monetaryIndicatorCategory
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MonetaryIndicatorCategoryCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            dto.Slugs = SlugHelper.GenerateSlugs(dto.Titles);
            var created = await _monetaryIndicatorCategoryService.CreateAsync(dto);

            if (!created)
            {
                Log.Warning("Monetar göstərici kateqoriyası məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dto);
                return BadRequest();
            }

            Log.Information("Monetar göstərici kateqoriyası məlumatı uğurla əlavə olundu : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/monetaryIndicatorCategory/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] MonetaryIndicatorCategoryEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            dto.Slugs = SlugHelper.GenerateSlugs(dto.Titles);
            var updated = await _monetaryIndicatorCategoryService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Warning("Monetar göstərici kateqoriyası məlumatı yenilənməsi uğursuz oldu : {@Dto}", dto);
                return NotFound();
            }
            Log.Information("Monetar göstərici kateqoriyası məlumatı yenilənməsi uğurludur : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });

        }

        // DELETE: /api/monetaryIndicatorCategory/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _monetaryIndicatorCategoryService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Monetar göstərici kateqoriyası məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Monetar göstərici kateqoriyası məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }
    }
}
