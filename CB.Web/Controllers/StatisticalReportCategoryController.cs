using CB.Application.DTOs.StatisticalReportCategory;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.StatisticalReportCategory;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class StatisticalReportCategoryController : ControllerBase
    {
        private readonly IStatisticalReportCategoryService _statisticalReportCategoryService;
        private readonly StatisticalReportCategoryCreateValidator _createValidator;
        private readonly StatisticalReportCategoryEditValidator _editValidator;

        public StatisticalReportCategoryController(
            IStatisticalReportCategoryService statisticalReportCategoryService,
            StatisticalReportCategoryCreateValidator createValidator,
            StatisticalReportCategoryEditValidator editValidator
        )
        {
            _statisticalReportCategoryService = statisticalReportCategoryService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/statisticalReportCategory
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _statisticalReportCategoryService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/statisticalReportCategory/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _statisticalReportCategoryService.GetByIdAsync(id);
            if (data == null)
            {
                Log.Warning("Statistika kateqoriya məlumatı tapılmadı. Id={Id}", id);
                return NotFound();
            }

            return Ok(data);
        }

        // POST: /api/statisticalReportCategory
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StatisticalReportCategoryCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _statisticalReportCategoryService.CreateAsync(dto);

            if (!created)
            {
                Log.Error("Statistika kateqoriya əlavə edilməsi uğursuz oldu {@Dto}", dto);
                return BadRequest(new { status = "error", message = "Məlumat əlavə olunmadı" });
            }

            Log.Information("Məlumat uğurla əlavə olundu {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/statisticalReportCategory/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] StatisticalReportCategoryEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });

            var updated = await _statisticalReportCategoryService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Error("Statistika kateqoriya yenilənməsi uğursuz oldu {@Dto}", dto);
                return NotFound();
            }

            Log.Information("Statistika kateqoriya yenilənməsi uğurludur {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        // DELETE: /api/statisticalReportCategory/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _statisticalReportCategoryService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Statistika kateqoriya silinməsi uğursuz oldu Id={@Id}", id);
                return NotFound();
            }
            Log.Information("Statistika kateqoriya uğurla silindi : Id={@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }
    }
}
