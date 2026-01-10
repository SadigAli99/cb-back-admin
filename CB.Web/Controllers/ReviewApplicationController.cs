using CB.Application.DTOs.ReviewApplication;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.ReviewApplication;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReviewApplicationController : ControllerBase
    {
        private readonly IReviewApplicationService _reviewApplicationService;
        private readonly ReviewApplicationCreateValidator _createValidator;
        private readonly ReviewApplicationEditValidator _editValidator;

        public ReviewApplicationController(
            IReviewApplicationService reviewApplicationService,
            ReviewApplicationCreateValidator createValidator,
            ReviewApplicationEditValidator editValidator
        )
        {
            _reviewApplicationService = reviewApplicationService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/reviewApplication
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _reviewApplicationService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/reviewApplication/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _reviewApplicationService.GetByIdAsync(id);
            if (data == null)
            {
                Log.Warning("İşçi məqaləsi məlumatı tapılmadı. Id={Id}", id);
                return NotFound();
            }

            return Ok(data);
        }

        // POST: /api/reviewApplication
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReviewApplicationCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _reviewApplicationService.CreateAsync(dto);

            if (!created)
            {
                Log.Error("İşçi məqaləsi əlavə edilməsi uğursuz oldu {@Dto}", dto);
                return BadRequest(new { status = "error", message = "Məlumat əlavə olunmadı" });
            }

            Log.Information("Məlumat uğurla əlavə olundu {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/reviewApplication/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] ReviewApplicationEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });

            var updated = await _reviewApplicationService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Error("İşçi məqaləsi yenilənməsi uğursuz oldu {@Dto}", dto);
                return NotFound();
            }

            Log.Information("İşçi məqaləsi yenilənməsi uğurludur {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        // DELETE: /api/reviewApplication/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _reviewApplicationService.DeleteAsync(id);
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
