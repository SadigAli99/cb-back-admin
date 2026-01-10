using CB.Application.DTOs.StaffArticle;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.StaffArticle;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class StaffArticleController : ControllerBase
    {
        private readonly IStaffArticleService _staffArticleService;
        private readonly StaffArticleCreateValidator _createValidator;
        private readonly StaffArticleEditValidator _editValidator;

        public StaffArticleController(
            IStaffArticleService staffArticleService,
            StaffArticleCreateValidator createValidator,
            StaffArticleEditValidator editValidator
        )
        {
            _staffArticleService = staffArticleService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/staffArticle
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _staffArticleService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/staffArticle/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _staffArticleService.GetByIdAsync(id);
            if (data == null)
            {
                Log.Warning("İşçi məqaləsi məlumatı tapılmadı. Id={Id}", id);
                return NotFound();
            }

            return Ok(data);
        }

        // POST: /api/staffArticle
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StaffArticleCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _staffArticleService.CreateAsync(dto);

            if (!created)
            {
                Log.Error("İşçi məqaləsi əlavə edilməsi uğursuz oldu {@Dto}", dto);
                return BadRequest(new { status = "error", message = "Məlumat əlavə olunmadı" });
            }

            Log.Information("Məlumat uğurla əlavə olundu {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/staffArticle/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] StaffArticleEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });

            var updated = await _staffArticleService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Error("İşçi məqaləsi yenilənməsi uğursuz oldu {@Dto}", dto);
                return NotFound();
            }

            Log.Information("İşçi məqaləsi yenilənməsi uğurludur {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        // DELETE: /api/staffArticle/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _staffArticleService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("İşçi məqaləsi silinməsi uğursuz oldu Id={@Id}", id);
                return NotFound();
            }
            Log.Information("İşçi məqaləsi uğurla silindi : Id={@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }
    }
}
