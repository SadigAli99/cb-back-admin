using CB.Application.DTOs.InternationalEvent;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.InternationalEvent;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InternationalEventController : ControllerBase
    {
        private readonly IInternationalEventService _internationalEventService;
        private readonly InternationalEventCreateValidator _createValidator;
        private readonly InternationalEventEditValidator _editValidator;

        public InternationalEventController(
            IInternationalEventService internationalEventService,
            InternationalEventCreateValidator createValidator,
            InternationalEventEditValidator editValidator
        )
        {
            _internationalEventService = internationalEventService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<InternationalEventGetDTO> data = await _internationalEventService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            InternationalEventGetDTO? data = await _internationalEventService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Beynəlxalq tədbir məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] InternationalEventCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _internationalEventService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Beynəlxalq tədbir məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Beynəlxalq tədbir məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] InternationalEventEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _internationalEventService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Beynəlxalq tədbir məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Beynəlxalq tədbir məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _internationalEventService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Beynəlxalq tədbir məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Beynəlxalq tədbir məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

        [HttpDelete("{internationalEventId}/images/{imageId}")]
        public async Task<IActionResult> DeleteImage(int internationalEventId, int imageId)
        {
            var result = await _internationalEventService.DeleteImageAsync(internationalEventId, imageId);
            if (!result)
            {
                Log.Warning("Beynəlxalq tədbir şəkli silinə bilmədi : InternationalEventId = {@InternationalEventId}, ImageId = {@ImageId}", internationalEventId, imageId);
                return NotFound(new { Message = "Şəkil tapılmadı və ya silinə bilmədi." });
            }

            Log.Warning("Beynəlxalq tədbir şəkli uğurla silindi : InternationalEventId = {@InternationalEventId}, ImageId = {@ImageId}", internationalEventId, imageId);
            return Ok(new { Message = "Şəkil uğurla silindi." });
        }

    }
}
