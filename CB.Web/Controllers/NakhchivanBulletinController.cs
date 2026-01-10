using CB.Application.DTOs.NakhchivanBulletin;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.NakhchivanBulletin;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NakhchivanBulletinController : ControllerBase
    {
        private readonly INakhchivanBulletinService _nakhchivanBulletinService;
        private readonly NakhchivanBulletinCreateValidator _createValidator;
        private readonly NakhchivanBulletinEditValidator _editValidator;

        public NakhchivanBulletinController(
            INakhchivanBulletinService nakhchivanBulletinService,
            NakhchivanBulletinCreateValidator createValidator,
            NakhchivanBulletinEditValidator editValidator
        )
        {
            _nakhchivanBulletinService = nakhchivanBulletinService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<NakhchivanBulletinGetDTO> data = await _nakhchivanBulletinService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            NakhchivanBulletinGetDTO? data = await _nakhchivanBulletinService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Statistik bülleten məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] NakhchivanBulletinCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _nakhchivanBulletinService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Statistik bülleten məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Statistik bülleten məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] NakhchivanBulletinEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _nakhchivanBulletinService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Statistik bülleten məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Statistik bülleten məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _nakhchivanBulletinService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Statistik bülleten məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Statistik bülleten məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
