using CB.Application.DTOs.Participant;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.Participant;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class ParticipantController : ControllerBase
    {
        private readonly IParticipantService _participantService;
        private readonly ParticipantCreateValidator _createValidator;
        private readonly ParticipantEditValidator _editValidator;

        public ParticipantController(
            IParticipantService participantService,
            ParticipantCreateValidator createValidator,
            ParticipantEditValidator editValidator
        )
        {
            _participantService = participantService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/participant
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _participantService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/participant/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _participantService.GetByIdAsync(id);
            if (data == null)
            {
                Log.Warning("İştirakçı məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }

            return Ok(new { status = "success", data });
        }

        // POST: /api/participant
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ParticipantCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _participantService.CreateAsync(dto);

            if (!created)
            {
                Log.Warning("İştirakçı məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dto);
                return BadRequest();
            }
            Log.Information("İştirakçı məlumatı uğurla əlavə olundu : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/participant/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] ParticipantEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var updated = await _participantService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Warning("İştirakçı məlumatı yenilənməsi uğursuz oldu : {@Dto}", dto);
                return NotFound();
            }
            Log.Information("İştirakçı məlumatı yenilənməsi uğurludur : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        // DELETE: /api/participant/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _participantService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("İştirakçı məlumatının silinməsi uğursuzdur : Id = {@Id}", id);

                return NotFound();
            }

            Log.Information("İştirakçı məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }
    }
}
