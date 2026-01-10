using CB.Application.DTOs.InsuranceStatistic;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.InsuranceStatistic;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InsuranceStatisticController : ControllerBase
    {
        private readonly IInsuranceStatisticService _insuranceStatisticService;
        private readonly InsuranceStatisticCreateValidator _createValidator;
        private readonly InsuranceStatisticEditValidator _editValidator;

        public InsuranceStatisticController(
            IInsuranceStatisticService insuranceStatisticService,
            InsuranceStatisticCreateValidator createValidator,
            InsuranceStatisticEditValidator editValidator
        )
        {
            _insuranceStatisticService = insuranceStatisticService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<InsuranceStatisticGetDTO> data = await _insuranceStatisticService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            InsuranceStatisticGetDTO? data = await _insuranceStatisticService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Sığorta məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] InsuranceStatisticCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _insuranceStatisticService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Sığorta məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Sığorta məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] InsuranceStatisticEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _insuranceStatisticService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Sığorta məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Sığorta məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _insuranceStatisticService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Sığorta məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Sığorta məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
