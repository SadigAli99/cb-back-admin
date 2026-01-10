using CB.Application.DTOs.CreditInstitutionCategory;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.CreditInstitutionCategory;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class CreditInstitutionCategoryController : ControllerBase
    {
        private readonly ICreditInstitutionCategoryService _creditInstitutionCategoryService;
        private readonly CreditInstitutionCategoryCreateValidator _createValidator;
        private readonly CreditInstitutionCategoryEditValidator _editValidator;

        public CreditInstitutionCategoryController(
            ICreditInstitutionCategoryService creditInstitutionCategoryService,
            CreditInstitutionCategoryCreateValidator createValidator,
            CreditInstitutionCategoryEditValidator editValidator
        )
        {
            _creditInstitutionCategoryService = creditInstitutionCategoryService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/creditInstitutionCategory
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _creditInstitutionCategoryService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/creditInstitutionCategory/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _creditInstitutionCategoryService.GetByIdAsync(id);
            if (data == null)
            {
                Log.Warning("Kredit təşkilatı kateqoriya məlumatı tapılmadı. Id={Id}", id);
                return NotFound();
            }

            return Ok(data);
        }

        // POST: /api/creditInstitutionCategory
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreditInstitutionCategoryCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _creditInstitutionCategoryService.CreateAsync(dto);

            if (!created)
            {
                Log.Error("Kredit təşkilatı kateqoriya əlavə edilməsi uğursuz oldu {@Dto}", dto);
                return BadRequest(new { status = "error", message = "Məlumat əlavə olunmadı" });
            }

            Log.Information("Məlumat uğurla əlavə olundu {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/creditInstitutionCategory/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] CreditInstitutionCategoryEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });

            var updated = await _creditInstitutionCategoryService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Error("Kredit təşkilatı kateqoriya yenilənməsi uğursuz oldu {@Dto}", dto);
                return NotFound();
            }

            Log.Information("Kredit təşkilatı kateqoriya yenilənməsi uğurludur {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        // DELETE: /api/creditInstitutionCategory/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _creditInstitutionCategoryService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Kredit təşkilatı kateqoriya silinməsi uğursuz oldu Id={@Id}", id);
                return NotFound();
            }
            Log.Information("Kredit təşkilatı kateqoriya uğurla silindi : Id={@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }
    }
}
