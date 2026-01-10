using CB.Application.DTOs.ReviewApplicationFile;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.ReviewApplicationFile;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReviewApplicationFileController : ControllerBase
    {
        private readonly IReviewApplicationFileService _reviewApplicationFileService;
        private readonly ReviewApplicationFileCreateValidator _createValidator;
        private readonly ReviewApplicationFileEditValidator _editValidator;

        public ReviewApplicationFileController(
            IReviewApplicationFileService reviewApplicationFileService,
            ReviewApplicationFileCreateValidator createValidator,
            ReviewApplicationFileEditValidator editValidator
        )
        {
            _reviewApplicationFileService = reviewApplicationFileService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/reviewApplicationFile
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _reviewApplicationFileService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/reviewApplicationFile/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _reviewApplicationFileService.GetByIdAsync(id);
            if (data == null)
            {
                Log.Warning("İşçi məqaləsi fayl məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }

            return Ok(new { status = "success", data });
        }

        // POST: /api/reviewApplicationFile
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ReviewApplicationFileCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _reviewApplicationFileService.CreateAsync(dto);

            if (!created)
            {
                Log.Warning("İşçi məqaləsi fayl məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dto);
                return BadRequest();
            }
            Log.Information("İşçi məqaləsi fayl məlumatı uğurla əlavə olundu : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/reviewApplicationFile/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] ReviewApplicationFileEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var updated = await _reviewApplicationFileService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Warning("İşçi məqaləsi fayl məlumatı yenilənməsi uğursuz oldu : {@Dto}", dto);
                return NotFound();
            }
            Log.Information("İşçi məqaləsi fayl məlumatı yenilənməsi uğurludur : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        // DELETE: /api/reviewApplicationFile/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _reviewApplicationFileService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("İşçi məqaləsi fayl məlumatının silinməsi uğursuzdur : Id = {@Id}", id);

                return NotFound();
            }

            Log.Information("İşçi məqaləsi fayl məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }
    }
}
