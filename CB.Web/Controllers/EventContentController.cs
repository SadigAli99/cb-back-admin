using CB.Application.DTOs.EventContent;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.EventContent;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EventContentController : ControllerBase
    {
        private readonly IEventContentService _eventContentService;
        private readonly EventContentCreateValidator _createValidator;
        private readonly EventContentEditValidator _editValidator;

        public EventContentController(
            IEventContentService eventContentService,
            EventContentCreateValidator createValidator,
            EventContentEditValidator editValidator
        )
        {
            _eventContentService = eventContentService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int id)
        {
            List<EventContentGetDTO> data = await _eventContentService.GetAllAsync(id);
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            EventContentGetDTO? data = await _eventContentService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Tədbir məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] EventContentCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _eventContentService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Tədbir məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Tədbir məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] EventContentEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _eventContentService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Tədbir məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Tədbir məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _eventContentService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Tədbir məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Tədbir məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }

        [HttpDelete("{eventContentId}/images/{imageId}")]
        public async Task<IActionResult> DeleteImage(int eventContentId, int imageId)
        {
            var result = await _eventContentService.DeleteImageAsync(eventContentId, imageId);
            if (!result)
            {
                Log.Warning("Tədbir şəkli silinə bilmədi : BlogId = {@BlogId}, ImageId = {@ImageId}", eventContentId, imageId);
                return NotFound(new { Message = "Şəkil tapılmadı və ya silinə bilmədi." });
            }
            Log.Information("Tədbir şəkli uğurla silindi : BlogId = {@BlogId}, ImageId = {@ImageId}", eventContentId, imageId);
            return Ok(new { Message = "Şəkil uğurla silindi." });
        }

    }
}
