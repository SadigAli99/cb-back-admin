using CB.Application.DTOs.ForeignInsuranceBroker;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.ForeignInsuranceBroker;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ForeignInsuranceBrokerController : ControllerBase
    {
        private readonly IForeignInsuranceBrokerService _foreignInsuranceBrokerService;
        private readonly ForeignInsuranceBrokerCreateValidator _createValidator;
        private readonly ForeignInsuranceBrokerEditValidator _editValidator;

        public ForeignInsuranceBrokerController(
            IForeignInsuranceBrokerService foreignInsuranceBrokerService,
            ForeignInsuranceBrokerCreateValidator createValidator,
            ForeignInsuranceBrokerEditValidator editValidator
        )
        {
            _foreignInsuranceBrokerService = foreignInsuranceBrokerService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<ForeignInsuranceBrokerGetDTO> data = await _foreignInsuranceBrokerService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            ForeignInsuranceBrokerGetDTO? data = await _foreignInsuranceBrokerService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Xarici sığorta vasitəçiləri məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ForeignInsuranceBrokerCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _foreignInsuranceBrokerService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Xarici sığorta vasitəçiləri məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Xarici sığorta vasitəçiləri məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] ForeignInsuranceBrokerEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _foreignInsuranceBrokerService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Xarici sığorta vasitəçiləri məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Xarici sığorta vasitəçiləri məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _foreignInsuranceBrokerService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Xarici sığorta vasitəçiləri məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Xarici sığorta vasitəçiləri məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
