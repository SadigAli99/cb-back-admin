using CB.Application.DTOs.InsuranceBroker;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.InsuranceBroker;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InsuranceBrokerController : ControllerBase
    {
        private readonly IInsuranceBrokerService _insuranceBrokerService;
        private readonly InsuranceBrokerCreateValidator _createValidator;
        private readonly InsuranceBrokerEditValidator _editValidator;

        public InsuranceBrokerController(
            IInsuranceBrokerService insuranceBrokerService,
            InsuranceBrokerCreateValidator createValidator,
            InsuranceBrokerEditValidator editValidator
        )
        {
            _insuranceBrokerService = insuranceBrokerService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<InsuranceBrokerGetDTO> data = await _insuranceBrokerService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            InsuranceBrokerGetDTO? data = await _insuranceBrokerService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Sığorta vasitəçiləri məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] InsuranceBrokerCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _insuranceBrokerService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Sığorta vasitəçiləri məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Sığorta vasitəçiləri məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] InsuranceBrokerEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _insuranceBrokerService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Sığorta vasitəçiləri məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Sığorta vasitəçiləri məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _insuranceBrokerService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Sığorta vasitəçiləri məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Sığorta vasitəçiləri məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
