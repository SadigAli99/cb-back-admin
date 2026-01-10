using CB.Application.DTOs.ControlMeasure;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.ControlMeasure;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class ControlMeasureController : ControllerBase
    {
        private readonly IControlMeasureService _controlMeasureService;
        private readonly ControlMeasureCreateValidator _createValidator;
        private readonly ControlMeasureEditValidator _editValidator;

        public ControlMeasureController(
            IControlMeasureService controlMeasureService,
            ControlMeasureCreateValidator createValidator,
            ControlMeasureEditValidator editValidator
            )
        {
            _controlMeasureService = controlMeasureService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /ControlMeasure
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _controlMeasureService.GetAllAsync();
            return Ok(data);
        }

        // POST: /ControlMeasure
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ControlMeasureCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool created = await _controlMeasureService.CreateAsync(dto);

            if (!created)
            {
                Log.Warning("Nəzarət tədbirləri məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dto);
                return BadRequest();
            }

            Log.Information("Nəzarət tədbirləri məlumatı uğurla əlavə olundu : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // GET: /ControlMeasure/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _controlMeasureService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Nəzarət tədbirləri məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        // PUT : /ControlMeasure/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, ControlMeasureEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            var updated = await _controlMeasureService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Warning("Nəzarət tədbirləri məlumatı yenilənməsi uğursuz oldu : {@Dto}", dto);
                return NotFound();
            }
            Log.Information("Nəzarət tədbirləri məlumatı yenilənməsi uğurludur : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });

        }

        // DELETE: /ControlMeasure/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _controlMeasureService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Nəzarət tədbirləri məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Nəzarət tədbirləri məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }
    }
}
