using CB.Application.DTOs.StatisticalReport;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.StatisticalReport;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize]
    public class StatisticalReportController : ControllerBase
    {
        private readonly IStatisticalReportService _statisticalReportService;
        private readonly StatisticalReportCreateValidator _createValidator;
        private readonly StatisticalReportEditValidator _editValidator;

        public StatisticalReportController(
            IStatisticalReportService statisticalReportService,
            StatisticalReportCreateValidator createValidator,
            StatisticalReportEditValidator editValidator
        )
        {
            _statisticalReportService = statisticalReportService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<StatisticalReportGetDTO> data = await _statisticalReportService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            StatisticalReportGetDTO? data = await _statisticalReportService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Statistika hesabatların yayılma qrafiki məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StatisticalReportCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _statisticalReportService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Statistika hesabatların yayılma qrafiki məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Statistika hesabatların yayılma qrafiki məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] StatisticalReportEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _statisticalReportService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Statistika hesabatların yayılma qrafiki məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Statistika hesabatların yayılma qrafiki məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _statisticalReportService.DeleteAsync(id);

            if (!deleted)
            {
                Log.Warning("Statistika hesabatların yayılma qrafiki məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Statistika hesabatların yayılma qrafiki məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });


        }

    }
}
