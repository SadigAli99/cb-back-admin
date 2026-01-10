using CB.Application.DTOs.InsuranceStatisticSubCategory;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.InsuranceStatisticSubCategory;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class InsuranceStatisticSubCategoryController : ControllerBase
    {
        private readonly IInsuranceStatisticSubCategoryService _insuranceStatisticSubCategoryService;
        private readonly InsuranceStatisticSubCategoryCreateValidator _createValidator;
        private readonly InsuranceStatisticSubCategoryEditValidator _editValidator;

        public InsuranceStatisticSubCategoryController(
            IInsuranceStatisticSubCategoryService insuranceStatisticSubCategoryService,
            InsuranceStatisticSubCategoryCreateValidator createValidator,
            InsuranceStatisticSubCategoryEditValidator editValidator
        )
        {
            _insuranceStatisticSubCategoryService = insuranceStatisticSubCategoryService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/insuranceStatisticSubCategory
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _insuranceStatisticSubCategoryService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/insuranceStatisticSubCategory/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _insuranceStatisticSubCategoryService.GetByIdAsync(id);
            if (data == null)
            {
                Log.Warning("Sığorta alt kateqoriya məlumatı tapılmadı. Id={Id}", id);
                return NotFound();
            }

            return Ok(data);
        }

        // POST: /api/insuranceStatisticSubCategory
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InsuranceStatisticSubCategoryCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _insuranceStatisticSubCategoryService.CreateAsync(dto);

            if (!created)
            {
                Log.Error("Sığorta alt kateqoriya əlavə edilməsi uğursuz oldu {@Dto}", dto);
                return BadRequest(new { status = "error", message = "Məlumat əlavə olunmadı" });
            }

            Log.Information("Məlumat uğurla əlavə olundu {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/insuranceStatisticSubCategory/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] InsuranceStatisticSubCategoryEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });

            var updated = await _insuranceStatisticSubCategoryService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Error("Sığorta alt kateqoriya yenilənməsi uğursuz oldu {@Dto}", dto);
                return NotFound();
            }

            Log.Information("Sığorta alt kateqoriya yenilənməsi uğurludur {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        // DELETE: /api/insuranceStatisticSubCategory/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _insuranceStatisticSubCategoryService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Sığorta alt kateqoriya silinməsi uğursuz oldu Id={@Id}", id);
                return NotFound();
            }
            Log.Information("Sığorta alt kateqoriya uğurla silindi : Id={@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }
    }
}
