using CB.Application.DTOs.PercentCorridor;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.PercentCorridor;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers.Admin
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class PercentCorridorController : ControllerBase
    {
        private readonly IPercentCorridorService _percentCorridorService;
        private readonly PercentCorridorCreateValidator _createValidator;
        private readonly PercentCorridorEditValidator _editValidator;

        public PercentCorridorController(
            IPercentCorridorService percentCorridorService,
            PercentCorridorCreateValidator createValidator,
            PercentCorridorEditValidator editValidator
            )
        {
            _percentCorridorService = percentCorridorService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /PercentCorridor
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _percentCorridorService.GetAllAsync();
            return Ok(data);
        }

        // POST: /PercentCorridor
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PercentCorridorCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool created = await _percentCorridorService.CreateAsync(dto);
            if (!created)
            {
                Log.Warning("Faiz dəhlizi məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dto);
                return BadRequest();
            }

            Log.Information("Faiz dəhlizi məlumatı uğurla əlavə olundu : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // GET: /PercentCorridor/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _percentCorridorService.GetForEditAsync(id);
            if (data is null)
            {
                Log.Warning("Faiz dəhlizi məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        // PUT : /PercentCorridor/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, PercentCorridorEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            var updated = await _percentCorridorService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Warning("Faiz dəhlizi məlumatı yenilənməsi uğursuz oldu : {@Dto}", dto);
                return NotFound();
            }
            Log.Information("Faiz dəhlizi məlumatı yenilənməsi uğurludur : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });

        }

        // DELETE: /PercentCorridor/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _percentCorridorService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Faiz dəhlizi məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Faiz dəhlizi məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }
    }
}
