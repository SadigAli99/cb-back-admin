using CB.Application.DTOs.FinancialFlow;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.FinancialFlow;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FinancialFlowController : ControllerBase
    {
        private readonly IFinancialFlowService _financialFlowService;
        private readonly FinancialFlowCreateValidator _createValidator;
        private readonly FinancialFlowEditValidator _editValidator;

        public FinancialFlowController(
            IFinancialFlowService financialFlowService,
            FinancialFlowCreateValidator createValidator,
            FinancialFlowEditValidator editValidator
        )
        {
            _financialFlowService = financialFlowService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<FinancialFlowGetDTO> data = await _financialFlowService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            FinancialFlowGetDTO? data = await _financialFlowService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Sahələrarası maliyyə axınlarının təhlili məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] FinancialFlowCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _financialFlowService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Sahələrarası maliyyə axınlarının təhlili məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Sahələrarası maliyyə axınlarının təhlili məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] FinancialFlowEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _financialFlowService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Sahələrarası maliyyə axınlarının təhlili məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Sahələrarası maliyyə axınlarının təhlili məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _financialFlowService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Sahələrarası maliyyə axınlarının təhlili məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Sahələrarası maliyyə axınlarının təhlili məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
