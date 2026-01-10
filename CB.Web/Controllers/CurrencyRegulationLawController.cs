using CB.Application.DTOs.CurrencyRegulationLaw;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.CurrencyRegulationLaw;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CurrencyRegulationLawController : ControllerBase
    {
        private readonly ICurrencyRegulationLawService _currencyRegulationLawService;
        private readonly CurrencyRegulationLawCreateValidator _createValidator;
        private readonly CurrencyRegulationLawEditValidator _editValidator;

        public CurrencyRegulationLawController(
            ICurrencyRegulationLawService currencyRegulationLawService,
            CurrencyRegulationLawCreateValidator createValidator,
            CurrencyRegulationLawEditValidator editValidator
        )
        {
            _currencyRegulationLawService = currencyRegulationLawService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<CurrencyRegulationLawGetDTO> data = await _currencyRegulationLawService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            CurrencyRegulationLawGetDTO? data = await _currencyRegulationLawService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Valyuta tənzimi qanunlar məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CurrencyRegulationLawCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _currencyRegulationLawService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Valyuta tənzimi qanunlar məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Valyuta tənzimi qanunlar məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] CurrencyRegulationLawEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _currencyRegulationLawService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Valyuta tənzimi qanunlar məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Valyuta tənzimi qanunlar məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _currencyRegulationLawService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Valyuta tənzimi qanunlar məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Valyuta tənzimi qanunlar məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
