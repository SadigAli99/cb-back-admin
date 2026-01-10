using CB.Application.DTOs.CitizenApplication;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.CitizenApplication;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class CitizenApplicationController : ControllerBase
    {
        private readonly ICitizenApplicationService _citizenApplicationService;
        private readonly CitizenApplicationCreateValidator _createValidator;
        private readonly CitizenApplicationEditValidator _editValidator;

        public CitizenApplicationController(
            ICitizenApplicationService citizenApplicationService,
            CitizenApplicationCreateValidator createValidator,
            CitizenApplicationEditValidator editValidator
        )
        {
            _citizenApplicationService = citizenApplicationService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/citizenApplication
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _citizenApplicationService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/citizenApplication/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _citizenApplicationService.GetByIdAsync(id);
            if (data == null)
            {
                Log.Warning("Vətəndaşların müraciət statistikası məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }

            return Ok(new { status = "success", data });
        }

        // POST: /api/citizenApplication
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CitizenApplicationCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _citizenApplicationService.CreateAsync(dto);

            if (!created)
            {
                Log.Warning("Vətəndaşların müraciət statistikası məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dto);
                return BadRequest();
            }
            Log.Information("Vətəndaşların müraciət statistikası məlumatı uğurla əlavə olundu : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/citizenApplication/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] CitizenApplicationEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var updated = await _citizenApplicationService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Warning("Vətəndaşların müraciət statistikası məlumatı yenilənməsi uğursuz oldu : {@Dto}", dto);
                return NotFound();
            }
            Log.Information("Vətəndaşların müraciət statistikası məlumatı yenilənməsi uğurludur : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        // DELETE: /api/citizenApplication/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _citizenApplicationService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Vətəndaşların müraciət statistikası məlumatının silinməsi uğursuzdur : Id = {@Id}", id);

                return NotFound();
            }

            Log.Information("Vətəndaşların müraciət statistikası məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }
    }
}
