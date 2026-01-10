using CB.Application.DTOs.FinancialStabilityReport;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.FinancialStabilityReport;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FinancialStabilityReportController : ControllerBase
    {
        private readonly IFinancialStabilityReportService _financialStabilityReportService;
        private readonly FinancialStabilityReportCreateValidator _createValidator;
        private readonly FinancialStabilityReportEditValidator _editValidator;

        public FinancialStabilityReportController(
            IFinancialStabilityReportService financialStabilityReportService,
            FinancialStabilityReportCreateValidator createValidator,
            FinancialStabilityReportEditValidator editValidator
        )
        {
            _financialStabilityReportService = financialStabilityReportService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<FinancialStabilityReportGetDTO> data = await _financialStabilityReportService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            FinancialStabilityReportGetDTO? data = await _financialStabilityReportService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Maliyyə sabitliyi hesabatı məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] FinancialStabilityReportCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _financialStabilityReportService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Maliyyə sabitliyi hesabatı məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Maliyyə sabitliyi hesabatı məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] FinancialStabilityReportEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _financialStabilityReportService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Maliyyə sabitliyi hesabatı məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Maliyyə sabitliyi hesabatı məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _financialStabilityReportService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Maliyyə sabitliyi hesabatı məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Maliyyə sabitliyi hesabatı məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
