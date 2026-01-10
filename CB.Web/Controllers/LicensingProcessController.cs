using CB.Application.DTOs.LicensingProcess;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.LicensingProcess;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LicensingProcessController : ControllerBase
    {
        private readonly ILicensingProcessService _licensingProcessService;
        private readonly LicensingProcessCreateValidator _createValidator;
        private readonly LicensingProcessEditValidator _editValidator;

        public LicensingProcessController(
            ILicensingProcessService licensingProcessService,
            LicensingProcessCreateValidator createValidator,
            LicensingProcessEditValidator editValidator
        )
        {
            _licensingProcessService = licensingProcessService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<LicensingProcessGetDTO> data = await _licensingProcessService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            LicensingProcessGetDTO? data = await _licensingProcessService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Lizensiya prosesləri məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] LicensingProcessCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _licensingProcessService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Lizensiya prosesləri məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Lizensiya prosesləri məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] LicensingProcessEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _licensingProcessService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Lizensiya prosesləri məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Lizensiya prosesləri məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _licensingProcessService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Lizensiya prosesləri məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Lizensiya prosesləri məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
