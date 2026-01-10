using CB.Application.DTOs.DigitalPaymentInfograhicCategory;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.DigitalPaymentInfograhicCategory;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class DigitalPaymentInfograhicCategoryController : ControllerBase
    {
        private readonly IDigitalPaymentInfograhicCategoryService _digitalPaymentInfographicCategoryService;
        private readonly DigitalPaymentInfograhicCategoryCreateValidator _createValidator;
        private readonly DigitalPaymentInfograhicCategoryEditValidator _editValidator;

        public DigitalPaymentInfograhicCategoryController(
            IDigitalPaymentInfograhicCategoryService digitalPaymentInfographicCategoryService,
            DigitalPaymentInfograhicCategoryCreateValidator createValidator,
            DigitalPaymentInfograhicCategoryEditValidator editValidator
        )
        {
            _digitalPaymentInfographicCategoryService = digitalPaymentInfographicCategoryService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/digitalPaymentInfographicCategory
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _digitalPaymentInfographicCategoryService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/digitalPaymentInfographicCategory/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _digitalPaymentInfographicCategoryService.GetByIdAsync(id);
            if (data == null)
            {
                Log.Warning("Rəqəmsal ödəniş infoqrafiyası kateqoriya məlumatı tapılmadı. Id={Id}", id);
                return NotFound();
            }

            return Ok(data);
        }

        // POST: /api/digitalPaymentInfographicCategory
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DigitalPaymentInfograhicCategoryCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _digitalPaymentInfographicCategoryService.CreateAsync(dto);

            if (!created)
            {
                Log.Error("Rəqəmsal ödəniş infoqrafiyası kateqoriya əlavə edilməsi uğursuz oldu {@Dto}", dto);
                return BadRequest(new { status = "error", message = "Məlumat əlavə olunmadı" });
            }

            Log.Information("Məlumat uğurla əlavə olundu {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/digitalPaymentInfographicCategory/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] DigitalPaymentInfograhicCategoryEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });

            var updated = await _digitalPaymentInfographicCategoryService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Error("Rəqəmsal ödəniş infoqrafiyası kateqoriya yenilənməsi uğursuz oldu {@Dto}", dto);
                return NotFound();
            }

            Log.Information("Rəqəmsal ödəniş infoqrafiyası kateqoriya yenilənməsi uğurludur {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        // DELETE: /api/digitalPaymentInfographicCategory/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _digitalPaymentInfographicCategoryService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Rəqəmsal ödəniş infoqrafiyası kateqoriya silinməsi uğursuz oldu Id={@Id}", id);
                return NotFound();
            }
            Log.Information("Rəqəmsal ödəniş infoqrafiyası kateqoriya uğurla silindi : Id={@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }
    }
}
