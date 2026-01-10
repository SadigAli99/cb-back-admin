using CB.Application.DTOs.BankSectorCategory;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.BankSectorCategory;
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
    public class BankSectorCategoryController : ControllerBase
    {
        private readonly IBankSectorCategoryService _monetaryIndicatorCategoryService;
        private readonly BankSectorCategoryCreateValidator _createValidator;
        private readonly BankSectorCategoryEditValidator _editValidator;

        public BankSectorCategoryController(
            IBankSectorCategoryService monetaryIndicatorCategoryService,
            BankSectorCategoryCreateValidator createValidator,
            BankSectorCategoryEditValidator editValidator
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
                Log.Warning("Bank sektor kateqoriya məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        // POST: /api/monetaryIndicatorCategory
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BankSectorCategoryCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            dto.Slugs = SlugHelper.GenerateSlugs(dto.Titles);
            var created = await _monetaryIndicatorCategoryService.CreateAsync(dto);

            if (!created)
            {
                Log.Warning("Bank sektor kateqoriya məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dto);
                return BadRequest();
            }

            Log.Information("Bank sektor kateqoriya məlumatı uğurla əlavə olundu : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/monetaryIndicatorCategory/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] BankSectorCategoryEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            dto.Slugs = SlugHelper.GenerateSlugs(dto.Titles);
            var updated = await _monetaryIndicatorCategoryService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Warning("Bank sektor kateqoriya məlumatı yenilənməsi uğursuz oldu : {@Dto}", dto);
                return NotFound();
            }
            Log.Information("Bank sektor kateqoriya məlumatı yenilənməsi uğurludur : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });

        }

        // DELETE: /api/monetaryIndicatorCategory/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _monetaryIndicatorCategoryService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Bank sektor kateqoriya məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Bank sektor kateqoriya məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }
    }
}
