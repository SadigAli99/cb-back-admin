using CB.Application.DTOs.Mission;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.Mission;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MissionController : ControllerBase
    {
        private readonly IMissionService _missionService;
        private readonly MissionCreateValidator _createValidator;
        private readonly MissionEditValidator _editValidator;

        public MissionController(
            IMissionService missionService,
            MissionCreateValidator createValidator,
            MissionEditValidator editValidator
        )
        {
            _missionService = missionService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<MissionGetDTO> data = await _missionService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            MissionGetDTO? data = await _missionService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Missiya və dəyərlər məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] MissionCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _missionService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Missiya və dəyərlər məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Missiya və dəyərlər məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] MissionEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _missionService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Missiya və dəyərlər məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Missiya və dəyərlər məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _missionService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Missiya və dəyərlər məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Missiya və dəyərlər məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
