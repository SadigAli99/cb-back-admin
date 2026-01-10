using CB.Application.DTOs.FinancialLiteracyEvent;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.FinancialLiteracyEvent;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FinancialLiteracyEventController : ControllerBase
    {
        private readonly IFinancialLiteracyEventService _financialLiteracyEventService;
        private readonly FinancialLiteracyEventCreateValidator _createValidator;
        private readonly FinancialLiteracyEventEditValidator _editValidator;

        public FinancialLiteracyEventController(
            IFinancialLiteracyEventService financialLiteracyEventService,
            FinancialLiteracyEventCreateValidator createValidator,
            FinancialLiteracyEventEditValidator editValidator
        )
        {
            _financialLiteracyEventService = financialLiteracyEventService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<FinancialLiteracyEventGetDTO> data = await _financialLiteracyEventService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            FinancialLiteracyEventGetDTO? data = await _financialLiteracyEventService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Tədbir məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FinancialLiteracyEventCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _financialLiteracyEventService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Tədbir məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Tədbir məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] FinancialLiteracyEventEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _financialLiteracyEventService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Tədbir məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Tədbir məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _financialLiteracyEventService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Tədbir məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Tədbir məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }

    }
}
