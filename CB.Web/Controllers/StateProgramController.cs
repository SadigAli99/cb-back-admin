using CB.Application.DTOs.StateProgram;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.StateProgram;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class StateProgramController : ControllerBase
    {
        private readonly IStateProgramService _stateProgramService;
        private readonly StateProgramCreateValidator _createValidator;
        private readonly StateProgramEditValidator _editValidator;

        public StateProgramController(
            IStateProgramService stateProgramService,
            StateProgramCreateValidator createValidator,
            StateProgramEditValidator editValidator
        )
        {
            _stateProgramService = stateProgramService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/stateProgram
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _stateProgramService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/stateProgram/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _stateProgramService.GetByIdAsync(id);
            if (data == null)
            {
                Log.Warning("Dövlət proqramı məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }

            return Ok(new { status = "success", data });
        }

        // POST: /api/stateProgram
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] StateProgramCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _stateProgramService.CreateAsync(dto);

            if (!created)
            {
                Log.Warning("Dövlət proqramı məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dto);
                return BadRequest();
            }
            Log.Information("Dövlət proqramı məlumatı uğurla əlavə olundu : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/stateProgram/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] StateProgramEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var updated = await _stateProgramService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Warning("Dövlət proqramı məlumatı yenilənməsi uğursuz oldu : {@Dto}", dto);
                return NotFound();
            }
            Log.Information("Dövlət proqramı məlumatı yenilənməsi uğurludur : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        // DELETE: /api/stateProgram/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _stateProgramService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Dövlət proqramı məlumatının silinməsi uğursuzdur : Id = {@Id}", id);

                return NotFound();
            }

            Log.Information("Dövlət proqramı məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }
    }
}
