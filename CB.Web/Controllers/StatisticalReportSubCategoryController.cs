using CB.Application.DTOs.StatisticalReportSubCategory;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.StatisticalReportSubCategory;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class StatisticalReportSubCategoryController : ControllerBase
    {
        private readonly IStatisticalReportSubCategoryService _statisticalReportSubCategoryService;
        private readonly StatisticalReportSubCategoryCreateValidator _createValidator;
        private readonly StatisticalReportSubCategoryEditValidator _editValidator;

        public StatisticalReportSubCategoryController(
            IStatisticalReportSubCategoryService statisticalReportSubCategoryService,
            StatisticalReportSubCategoryCreateValidator createValidator,
            StatisticalReportSubCategoryEditValidator editValidator
        )
        {
            _statisticalReportSubCategoryService = statisticalReportSubCategoryService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/statisticalReportSubCategory
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _statisticalReportSubCategoryService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/statisticalReportSubCategory/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _statisticalReportSubCategoryService.GetByIdAsync(id);
            if (data == null)
            {
                Log.Warning("Statistika alt kateqoriya məlumatı tapılmadı. Id={Id}", id);
                return NotFound();
            }

            return Ok(data);
        }

        // POST: /api/statisticalReportSubCategory
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StatisticalReportSubCategoryCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _statisticalReportSubCategoryService.CreateAsync(dto);

            if (!created)
            {
                Log.Error("Statistika alt kateqoriya əlavə edilməsi uğursuz oldu {@Dto}", dto);
                return BadRequest(new { status = "error", message = "Məlumat əlavə olunmadı" });
            }

            Log.Information("Məlumat uğurla əlavə olundu {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/statisticalReportSubCategory/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] StatisticalReportSubCategoryEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });

            var updated = await _statisticalReportSubCategoryService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Error("Statistika alt kateqoriya yenilənməsi uğursuz oldu {@Dto}", dto);
                return NotFound();
            }

            Log.Information("Statistika alt kateqoriya yenilənməsi uğurludur {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        // DELETE: /api/statisticalReportSubCategory/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _statisticalReportSubCategoryService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Statistika alt kateqoriya silinməsi uğursuz oldu Id={@Id}", id);
                return NotFound();
            }
            Log.Information("Statistika alt kateqoriya uğurla silindi : Id={@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }
    }
}
