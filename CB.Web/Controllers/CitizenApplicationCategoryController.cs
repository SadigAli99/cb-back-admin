using CB.Application.DTOs.CitizenApplicationCategory;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.CitizenApplicationCategory;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class CitizenApplicationCategoryController : ControllerBase
    {
        private readonly ICitizenApplicationCategoryService _citizenApplicationCategoryService;
        private readonly CitizenApplicationCategoryCreateValidator _createValidator;
        private readonly CitizenApplicationCategoryEditValidator _editValidator;

        public CitizenApplicationCategoryController(
            ICitizenApplicationCategoryService citizenApplicationCategoryService,
            CitizenApplicationCategoryCreateValidator createValidator,
            CitizenApplicationCategoryEditValidator editValidator
        )
        {
            _citizenApplicationCategoryService = citizenApplicationCategoryService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/citizenApplicationCategory
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _citizenApplicationCategoryService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/citizenApplicationCategory/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _citizenApplicationCategoryService.GetByIdAsync(id);
            if (data == null)
            {
                Log.Warning("Vətəndaşların müraciət kateqoriyası məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }

            return Ok(new { status = "success", data });
        }

        // POST: /api/citizenApplicationCategory
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CitizenApplicationCategoryCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _citizenApplicationCategoryService.CreateAsync(dto);

            if (!created)
            {
                Log.Warning("Vətəndaşların müraciət kateqoriyası məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dto);
                return BadRequest();
            }
            Log.Information("Vətəndaşların müraciət kateqoriyası məlumatı uğurla əlavə olundu : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/citizenApplicationCategory/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] CitizenApplicationCategoryEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var updated = await _citizenApplicationCategoryService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Warning("Vətəndaşların müraciət kateqoriyası məlumatı yenilənməsi uğursuz oldu : {@Dto}", dto);
                return NotFound();
            }
            Log.Information("Vətəndaşların müraciət kateqoriyası məlumatı yenilənməsi uğurludur : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        // DELETE: /api/citizenApplicationCategory/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _citizenApplicationCategoryService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Vətəndaşların müraciət kateqoriyası məlumatının silinməsi uğursuzdur : Id = {@Id}", id);

                return NotFound();
            }

            Log.Information("Vətəndaşların müraciət kateqoriyası məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }
    }
}
