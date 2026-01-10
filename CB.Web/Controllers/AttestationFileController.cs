using CB.Application.DTOs.AttestationFile;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.AttestationFile;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class AttestationFileController : ControllerBase
    {
        private readonly IAttestationFileService _attestationFileService;
        private readonly AttestationFileCreateValidator _createValidator;
        private readonly AttestationFileEditValidator _editValidator;

        public AttestationFileController(
            IAttestationFileService attestationFileService,
            AttestationFileCreateValidator createValidator,
            AttestationFileEditValidator editValidator
        )
        {
            _attestationFileService = attestationFileService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/attestationFile
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _attestationFileService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/attestationFile/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _attestationFileService.GetByIdAsync(id);
            if (data == null)
            {
                Log.Warning("Attestasiya fayl məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }

            return Ok(new { status = "success", data });
        }

        // POST: /api/attestationFile
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] AttestationFileCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _attestationFileService.CreateAsync(dto);

            if (!created)
            {
                Log.Warning("Attestasiya fayl məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dto);
                return BadRequest();
            }
            Log.Information("Attestasiya fayl məlumatı uğurla əlavə olundu : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/attestationFile/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] AttestationFileEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var updated = await _attestationFileService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Warning("Attestasiya fayl məlumatı yenilənməsi uğursuz oldu : {@Dto}", dto);
                return NotFound();
            }
            Log.Information("Attestasiya fayl məlumatı yenilənməsi uğurludur : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        // DELETE: /api/attestationFile/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _attestationFileService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Attestasiya fayl məlumatının silinməsi uğursuzdur : Id = {@Id}", id);

                return NotFound();
            }

            Log.Information("Attestasiya fayl məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }
    }
}
