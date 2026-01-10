using CB.Application.DTOs.Actuary;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.Actuary;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ActuaryController : ControllerBase
    {
        private readonly IActuaryService _actuaryService;
        private readonly ActuaryCreateValidator _createValidator;
        private readonly ActuaryEditValidator _editValidator;

        public ActuaryController(
            IActuaryService actuaryService,
            ActuaryCreateValidator createValidator,
            ActuaryEditValidator editValidator
        )
        {
            _actuaryService = actuaryService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<ActuaryGetDTO> data = await _actuaryService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            ActuaryGetDTO? data = await _actuaryService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Aktuari məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ActuaryCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _actuaryService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Aktuari məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Aktuari məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] ActuaryEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _actuaryService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Aktuari məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Aktuari məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _actuaryService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Aktuari məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Aktuari məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
