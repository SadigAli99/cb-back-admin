using CB.Application.DTOs.NakhchivanPublication;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.NakhchivanPublication;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NakhchivanPublicationController : ControllerBase
    {
        private readonly INakhchivanPublicationService _nakhchivanPublicationService;
        private readonly NakhchivanPublicationCreateValidator _createValidator;
        private readonly NakhchivanPublicationEditValidator _editValidator;

        public NakhchivanPublicationController(
            INakhchivanPublicationService nakhchivanPublicationService,
            NakhchivanPublicationCreateValidator createValidator,
            NakhchivanPublicationEditValidator editValidator
        )
        {
            _nakhchivanPublicationService = nakhchivanPublicationService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<NakhchivanPublicationGetDTO> data = await _nakhchivanPublicationService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            NakhchivanPublicationGetDTO? data = await _nakhchivanPublicationService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Nəşr məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] NakhchivanPublicationCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _nakhchivanPublicationService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Nəşr məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Nəşr məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] NakhchivanPublicationEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _nakhchivanPublicationService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Nəşr məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Nəşr məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _nakhchivanPublicationService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Nəşr məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Nəşr məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
