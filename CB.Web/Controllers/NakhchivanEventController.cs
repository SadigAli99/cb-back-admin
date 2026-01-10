using CB.Application.DTOs.NakhchivanEvent;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.NakhchivanEvent;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NakhchivanEventController : ControllerBase
    {
        private readonly INakhchivanEventService _nakhchivanEventService;
        private readonly NakhchivanEventCreateValidator _createValidator;
        private readonly NakhchivanEventEditValidator _editValidator;

        public NakhchivanEventController(
            INakhchivanEventService nakhchivanEventService,
            NakhchivanEventCreateValidator createValidator,
            NakhchivanEventEditValidator editValidator
        )
        {
            _nakhchivanEventService = nakhchivanEventService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<NakhchivanEventGetDTO> data = await _nakhchivanEventService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            NakhchivanEventGetDTO? data = await _nakhchivanEventService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Naxçıvan bloq məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] NakhchivanEventCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _nakhchivanEventService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Naxçıvan bloq məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Naxçıvan bloq məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] NakhchivanEventEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _nakhchivanEventService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Naxçıvan bloq məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Naxçıvan bloq məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _nakhchivanEventService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Naxçıvan bloq məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Naxçıvan bloq məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

        [HttpDelete("{nakhchivanEventId}/images/{imageId}")]
        public async Task<IActionResult> DeleteImage(int nakhchivanEventId, int imageId)
        {
            var result = await _nakhchivanEventService.DeleteImageAsync(nakhchivanEventId, imageId);
            if (!result)
            {
                Log.Warning("Naxçıvan bloq şəkli silinə bilmədi : nakhchivanEventId = {@nakhchivanEventId}, ImageId = {@ImageId}", nakhchivanEventId, imageId);
                return NotFound(new { Message = "Şəkil tapılmadı və ya silinə bilmədi." });
            }

            Log.Warning("Naxçıvan bloq şəkli uğurla silindi : nakhchivanEventId = {@nakhchivanEventId}, ImageId = {@ImageId}", nakhchivanEventId, imageId);
            return Ok(new { Message = "Şəkil uğurla silindi." });
        }

    }
}
