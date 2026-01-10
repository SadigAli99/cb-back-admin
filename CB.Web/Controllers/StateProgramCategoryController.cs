using CB.Application.DTOs.StateProgramCategory;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.StateProgramCategory;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class StateProgramCategoryController : ControllerBase
    {
        private readonly IStateProgramCategoryService _stateProgramCategoryService;
        private readonly StateProgramCategoryCreateValidator _createValidator;
        private readonly StateProgramCategoryEditValidator _editValidator;

        public StateProgramCategoryController(
            IStateProgramCategoryService stateProgramCategoryService,
            StateProgramCategoryCreateValidator createValidator,
            StateProgramCategoryEditValidator editValidator
        )
        {
            _stateProgramCategoryService = stateProgramCategoryService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/stateProgramCategory
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _stateProgramCategoryService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/stateProgramCategory/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _stateProgramCategoryService.GetByIdAsync(id);
            if (data == null)
            {
                Log.Warning("Dövlət proqramı kateqoriyası məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }

            return Ok(new { status = "success", data });
        }

        // POST: /api/stateProgramCategory
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StateProgramCategoryCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _stateProgramCategoryService.CreateAsync(dto);

            if (!created)
            {
                Log.Warning("Dövlət proqramı kateqoriyası məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dto);
                return BadRequest();
            }
            Log.Information("Dövlət proqramı kateqoriyası məlumatı uğurla əlavə olundu : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/stateProgramCategory/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] StateProgramCategoryEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var updated = await _stateProgramCategoryService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Warning("Dövlət proqramı kateqoriyası məlumatı yenilənməsi uğursuz oldu : {@Dto}", dto);
                return NotFound();
            }
            Log.Information("Dövlət proqramı kateqoriyası məlumatı yenilənməsi uğurludur : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        // DELETE: /api/stateProgramCategory/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _stateProgramCategoryService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Dövlət proqramı kateqoriyası məlumatının silinməsi uğursuzdur : Id = {@Id}", id);

                return NotFound();
            }

            Log.Information("Dövlət proqramı kateqoriyası məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }
    }
}
