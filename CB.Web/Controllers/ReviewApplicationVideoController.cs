using CB.Application.DTOs.ReviewApplicationVideo;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.ReviewApplicationVideo;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReviewApplicationVideoController : ControllerBase
    {
        private readonly IReviewApplicationVideoService _reviewApplicationVideoService;
        private readonly ReviewApplicationVideoCreateValidator _createValidator;
        private readonly ReviewApplicationVideoEditValidator _editValidator;

        public ReviewApplicationVideoController(
            IReviewApplicationVideoService reviewApplicationVideoService,
            ReviewApplicationVideoCreateValidator createValidator,
            ReviewApplicationVideoEditValidator editValidator
        )
        {
            _reviewApplicationVideoService = reviewApplicationVideoService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<ReviewApplicationVideoGetDTO> data = await _reviewApplicationVideoService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            ReviewApplicationVideoGetDTO? data = await _reviewApplicationVideoService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Vətəndaşların qeydiyyatı video məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReviewApplicationVideoCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _reviewApplicationVideoService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Vətəndaşların qeydiyyatı video məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Vətəndaşların qeydiyyatı video məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] ReviewApplicationVideoEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _reviewApplicationVideoService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Vətəndaşların qeydiyyatı video məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Vətəndaşların qeydiyyatı video məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _reviewApplicationVideoService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Vətəndaşların qeydiyyatı video məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Vətəndaşların qeydiyyatı video məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
