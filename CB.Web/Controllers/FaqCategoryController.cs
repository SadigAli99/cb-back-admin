using CB.Application.DTOs.FaqCategory;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.FaqCategory;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class FaqCategoryController : ControllerBase
    {
        private readonly IFaqCategoryService _faqCategoryService;
        private readonly FaqCategoryCreateValidator _createValidator;
        private readonly FaqCategoryEditValidator _editValidator;

        public FaqCategoryController(
            IFaqCategoryService faqCategoryService,
            FaqCategoryCreateValidator createValidator,
            FaqCategoryEditValidator editValidator
        )
        {
            _faqCategoryService = faqCategoryService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/faqCategory
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _faqCategoryService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/faqCategory/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _faqCategoryService.GetByIdAsync(id);
            if (data == null)
            {
                Log.Warning("Tez-tez verilən suallar kateqoriya məlumatı tapılmadı. Id={Id}", id);
                return NotFound();
            }

            return Ok(data);
        }

        // POST: /api/faqCategory
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FaqCategoryCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _faqCategoryService.CreateAsync(dto);

            if (!created)
            {
                Log.Error("Tez-tez verilən suallar kateqoriya əlavə edilməsi uğursuz oldu {@Dto}", dto);
                return BadRequest(new { status = "error", message = "Məlumat əlavə olunmadı" });
            }

            Log.Information("Məlumat uğurla əlavə olundu {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/faqCategory/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] FaqCategoryEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });

            var updated = await _faqCategoryService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Error("Tez-tez verilən suallar kateqoriya yenilənməsi uğursuz oldu {@Dto}", dto);
                return NotFound();
            }

            Log.Information("Tez-tez verilən suallar kateqoriya yenilənməsi uğurludur {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        // DELETE: /api/faqCategory/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _faqCategoryService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Tez-tez verilən suallar kateqoriya silinməsi uğursuz oldu Id={@Id}", id);
                return NotFound();
            }
            Log.Information("Tez-tez verilən suallar kateqoriya uğurla silindi : Id={@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }
    }
}
