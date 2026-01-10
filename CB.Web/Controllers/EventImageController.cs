using CB.Application.DTOs.EventImage;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.EventImage;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EventImageController : ControllerBase
    {
        private readonly IEventImageService _eventImageService;
        private readonly EventImageCreateValidator _createValidator;

        public EventImageController(
            IEventImageService eventImageService,
            EventImageCreateValidator createValidator
        )
        {
            _eventImageService = eventImageService;
            _createValidator = createValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int id)
        {
            List<EventImageGetDTO> data = await _eventImageService.GetAllAsync(id);
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            EventImageGetDTO? data = await _eventImageService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Tədbir şəkil məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] EventImageCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _eventImageService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Tədbir şəkil məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Tədbir şəkil məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _eventImageService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Tədbir şəkil məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Tədbir şəkil məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
