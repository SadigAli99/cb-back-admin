using CB.Application.DTOs.Nomination;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.Nomination;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class NominationController : ControllerBase
    {
        private readonly INominationService _nominationService;
        private readonly NominationCreateValidator _createValidator;
        private readonly NominationEditValidator _editValidator;

        public NominationController(
            INominationService nominationService,
            NominationCreateValidator createValidator,
            NominationEditValidator editValidator
        )
        {
            _nominationService = nominationService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/nomination
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _nominationService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/nomination/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _nominationService.GetByIdAsync(id);
            if (data == null)
            {
                Log.Warning("Nominasiya məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }

            return Ok(new { status = "success", data });
        }

        // POST: /api/nomination
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NominationCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _nominationService.CreateAsync(dto);

            if (!created)
            {
                Log.Warning("Nominasiya məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dto);
                return BadRequest();
            }
            Log.Information("Nominasiya məlumatı uğurla əlavə olundu : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/nomination/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] NominationEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var updated = await _nominationService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Warning("Nominasiya məlumatı yenilənməsi uğursuz oldu : {@Dto}", dto);
                return NotFound();
            }
            Log.Information("Nominasiya məlumatı yenilənməsi uğurludur : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        // DELETE: /api/nomination/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _nominationService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Nominasiya məlumatının silinməsi uğursuzdur : Id = {@Id}", id);

                return NotFound();
            }

            Log.Information("Nominasiya məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }
    }
}
