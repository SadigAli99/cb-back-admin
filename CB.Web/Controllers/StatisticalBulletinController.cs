using CB.Application.DTOs.StatisticalBulletin;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.StatisticalBulletin;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class StatisticalBulletinController : ControllerBase
    {
        private readonly IStatisticalBulletinService _statisticalBulletinService;
        private readonly StatisticalBulletinCreateValidator _createValidator;
        private readonly StatisticalBulletinEditValidator _editValidator;

        public StatisticalBulletinController(
            IStatisticalBulletinService statisticalBulletinService,
            StatisticalBulletinCreateValidator createValidator,
            StatisticalBulletinEditValidator editValidator
        )
        {
            _statisticalBulletinService = statisticalBulletinService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<StatisticalBulletinGetDTO> data = await _statisticalBulletinService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            StatisticalBulletinGetDTO? data = await _statisticalBulletinService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Statistik bülleten məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] StatisticalBulletinCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _statisticalBulletinService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Statistik bülleten məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Statistik bülleten məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] StatisticalBulletinEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _statisticalBulletinService.UpdateAsync(id, dTO);
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
            var deleted = await _statisticalBulletinService.DeleteAsync(id);
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
