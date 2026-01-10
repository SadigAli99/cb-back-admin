using CB.Application.DTOs.NominationCategory;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.NominationCategory;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class NominationCategoryController : ControllerBase
    {
        private readonly INominationCategoryService _nominationCategoryService;
        private readonly NominationCategoryCreateValidator _createValidator;
        private readonly NominationCategoryEditValidator _editValidator;

        public NominationCategoryController(
            INominationCategoryService nominationCategoryService,
            NominationCategoryCreateValidator createValidator,
            NominationCategoryEditValidator editValidator
        )
        {
            _nominationCategoryService = nominationCategoryService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/nominationCategory
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _nominationCategoryService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/nominationCategory/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _nominationCategoryService.GetByIdAsync(id);
            if (data == null)
            {
                Log.Warning("Nominasiya kateqoriyası məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }

            return Ok(new { status = "success", data });
        }

        // POST: /api/nominationCategory
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NominationCategoryCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _nominationCategoryService.CreateAsync(dto);

            if (!created)
            {
                Log.Warning("Nominasiya kateqoriyası məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dto);
                return BadRequest();
            }
            Log.Information("Nominasiya kateqoriyası məlumatı uğurla əlavə olundu : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/nominationCategory/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] NominationCategoryEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var updated = await _nominationCategoryService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Warning("Nominasiya kateqoriyası məlumatı yenilənməsi uğursuz oldu : {@Dto}", dto);
                return NotFound();
            }
            Log.Information("Nominasiya kateqoriyası məlumatı yenilənməsi uğurludur : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        // DELETE: /api/nominationCategory/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _nominationCategoryService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Nominasiya kateqoriyası məlumatının silinməsi uğursuzdur : Id = {@Id}", id);

                return NotFound();
            }

            Log.Information("Nominasiya kateqoriyası məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }
    }
}
