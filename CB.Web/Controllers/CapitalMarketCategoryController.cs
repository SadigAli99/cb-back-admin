using CB.Application.DTOs.CapitalMarketCategory;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.CapitalMarketCategory;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class CapitalMarketCategoryController : ControllerBase
    {
        private readonly ICapitalMarketCategoryService _capitalMarketCategoryService;
        private readonly CapitalMarketCategoryCreateValidator _createValidator;
        private readonly CapitalMarketCategoryEditValidator _editValidator;

        public CapitalMarketCategoryController(
            ICapitalMarketCategoryService capitalMarketCategoryService,
            CapitalMarketCategoryCreateValidator createValidator,
            CapitalMarketCategoryEditValidator editValidator
        )
        {
            _capitalMarketCategoryService = capitalMarketCategoryService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/capitalMarketCategory
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _capitalMarketCategoryService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/capitalMarketCategory/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _capitalMarketCategoryService.GetByIdAsync(id);
            if (data == null)
            {
                Log.Warning("Kapital bazarı kateqoriya məlumatı tapılmadı. Id={Id}", id);
                return NotFound();
            }

            return Ok(data);
        }

        // POST: /api/capitalMarketCategory
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CapitalMarketCategoryCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _capitalMarketCategoryService.CreateAsync(dto);

            if (!created)
            {
                Log.Error("Kapital bazarı kateqoriya əlavə edilməsi uğursuz oldu {@Dto}", dto);
                return BadRequest(new { status = "error", message = "Məlumat əlavə olunmadı" });
            }

            Log.Information("Məlumat uğurla əlavə olundu {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/capitalMarketCategory/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] CapitalMarketCategoryEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });

            var updated = await _capitalMarketCategoryService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Error("Kapital bazarı kateqoriya yenilənməsi uğursuz oldu {@Dto}", dto);
                return NotFound();
            }

            Log.Information("Kapital bazarı kateqoriya yenilənməsi uğurludur {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        // DELETE: /api/capitalMarketCategory/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _capitalMarketCategoryService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Kapital bazarı kateqoriya silinməsi uğursuz oldu Id={@Id}", id);
                return NotFound();
            }
            Log.Information("Kapital bazarı kateqoriya uğurla silindi : Id={@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }
    }
}
