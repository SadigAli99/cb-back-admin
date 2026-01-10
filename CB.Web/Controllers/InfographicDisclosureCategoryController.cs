using CB.Application.DTOs.InfographicDisclosureCategory;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.InfographicDisclosureCategory;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class InfographicDisclosureCategoryController : ControllerBase
    {
        private readonly IInfographicDisclosureCategoryService _infographicDisclosureCategoryService;
        private readonly InfographicDisclosureCategoryCreateValidator _createValidator;
        private readonly InfographicDisclosureCategoryEditValidator _editValidator;

        public InfographicDisclosureCategoryController(
            IInfographicDisclosureCategoryService infographicDisclosureCategoryService,
            InfographicDisclosureCategoryCreateValidator createValidator,
            InfographicDisclosureCategoryEditValidator editValidator
        )
        {
            _infographicDisclosureCategoryService = infographicDisclosureCategoryService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/infographicDisclosureCategory
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _infographicDisclosureCategoryService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/infographicDisclosureCategory/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _infographicDisclosureCategoryService.GetByIdAsync(id);
            if (data == null)
            {
                Log.Warning("Məlumatların yayımlanması infoqrafiyası kateqoriya məlumatı tapılmadı. Id={Id}", id);
                return NotFound();
            }

            return Ok(data);
        }

        // POST: /api/infographicDisclosureCategory
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InfographicDisclosureCategoryCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _infographicDisclosureCategoryService.CreateAsync(dto);

            if (!created)
            {
                Log.Error("Məlumatların yayımlanması infoqrafiyası kateqoriya əlavə edilməsi uğursuz oldu {@Dto}", dto);
                return BadRequest(new { status = "error", message = "Məlumat əlavə olunmadı" });
            }

            Log.Information("Məlumat uğurla əlavə olundu {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/infographicDisclosureCategory/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] InfographicDisclosureCategoryEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });

            var updated = await _infographicDisclosureCategoryService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Error("Məlumatların yayımlanması infoqrafiyası kateqoriya yenilənməsi uğursuz oldu {@Dto}", dto);
                return NotFound();
            }

            Log.Information("Məlumatların yayımlanması infoqrafiyası kateqoriya yenilənməsi uğurludur {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        // DELETE: /api/infographicDisclosureCategory/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _infographicDisclosureCategoryService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Məlumatların yayımlanması infoqrafiyası kateqoriya silinməsi uğursuz oldu Id={@Id}", id);
                return NotFound();
            }
            Log.Information("Məlumatların yayımlanması infoqrafiyası kateqoriya uğurla silindi : Id={@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }
    }
}
