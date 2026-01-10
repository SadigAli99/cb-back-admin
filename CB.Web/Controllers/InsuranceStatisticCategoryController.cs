using CB.Application.DTOs.InsuranceStatisticCategory;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.InsuranceStatisticCategory;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class InsuranceStatisticCategoryController : ControllerBase
    {
        private readonly IInsuranceStatisticCategoryService _insuranceStatisticCategoryService;
        private readonly InsuranceStatisticCategoryCreateValidator _createValidator;
        private readonly InsuranceStatisticCategoryEditValidator _editValidator;

        public InsuranceStatisticCategoryController(
            IInsuranceStatisticCategoryService insuranceStatisticCategoryService,
            InsuranceStatisticCategoryCreateValidator createValidator,
            InsuranceStatisticCategoryEditValidator editValidator
        )
        {
            _insuranceStatisticCategoryService = insuranceStatisticCategoryService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/insuranceStatisticCategory
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _insuranceStatisticCategoryService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/insuranceStatisticCategory/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _insuranceStatisticCategoryService.GetByIdAsync(id);
            if (data == null)
            {
                Log.Warning("Sığorta kateqoriya məlumatı tapılmadı. Id={Id}", id);
                return NotFound();
            }

            return Ok(data);
        }

        // POST: /api/insuranceStatisticCategory
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InsuranceStatisticCategoryCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _insuranceStatisticCategoryService.CreateAsync(dto);

            if (!created)
            {
                Log.Error("Sığorta kateqoriya əlavə edilməsi uğursuz oldu {@Dto}", dto);
                return BadRequest(new { status = "error", message = "Məlumat əlavə olunmadı" });
            }

            Log.Information("Məlumat uğurla əlavə olundu {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/insuranceStatisticCategory/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] InsuranceStatisticCategoryEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });

            var updated = await _insuranceStatisticCategoryService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Error("Sığorta kateqoriya yenilənməsi uğursuz oldu {@Dto}", dto);
                return NotFound();
            }

            Log.Information("Sığorta kateqoriya yenilənməsi uğurludur {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        // DELETE: /api/insuranceStatisticCategory/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _insuranceStatisticCategoryService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Sığorta kateqoriya silinməsi uğursuz oldu Id={@Id}", id);
                return NotFound();
            }
            Log.Information("Sığorta kateqoriya uğurla silindi : Id={@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }
    }
}
