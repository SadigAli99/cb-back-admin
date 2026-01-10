using CB.Application.DTOs.StatisticalReportFile;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.StatisticalReportFile;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class StatisticalReportFileController : ControllerBase
    {
        private readonly IStatisticalReportFileService _statisticReportFileService;
        private readonly StatisticalReportFileCreateValidator _createValidator;
        private readonly StatisticalReportFileEditValidator _editValidator;

        public StatisticalReportFileController(
            IStatisticalReportFileService statisticReportFileService,
            StatisticalReportFileCreateValidator createValidator,
            StatisticalReportFileEditValidator editValidator
        )
        {
            _statisticReportFileService = statisticReportFileService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<StatisticalReportFileGetDTO> data = await _statisticReportFileService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            StatisticalReportFileGetDTO? data = await _statisticReportFileService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Statistik hesabat fayl məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] StatisticalReportFileCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _statisticReportFileService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Statistik hesabat fayl məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Statistik hesabat fayl məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] StatisticalReportFileEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _statisticReportFileService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Statistik hesabat fayl məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Statistik hesabat fayl məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _statisticReportFileService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Statistik hesabat fayl məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Statistik hesabat fayl məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
