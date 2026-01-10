using CB.Application.DTOs.CurrencyRegulationRight;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.CurrencyRegulationRight;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CurrencyRegulationRightController : ControllerBase
    {
        private readonly ICurrencyRegulationRightService _currencyRegulationService;
        private readonly CurrencyRegulationRightCreateValidator _createValidator;
        private readonly CurrencyRegulationRightEditValidator _editValidator;

        public CurrencyRegulationRightController(
            ICurrencyRegulationRightService currencyRegulationService,
            CurrencyRegulationRightCreateValidator createValidator,
            CurrencyRegulationRightEditValidator editValidator
        )
        {
            _currencyRegulationService = currencyRegulationService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<CurrencyRegulationRightGetDTO> data = await _currencyRegulationService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            CurrencyRegulationRightGetDTO? data = await _currencyRegulationService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Valyuta tənzimi qaydalar məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CurrencyRegulationRightCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _currencyRegulationService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Valyuta tənzimi qaydalar məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Valyuta tənzimi qaydalar məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] CurrencyRegulationRightEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _currencyRegulationService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Valyuta tənzimi qaydalar məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Valyuta tənzimi qaydalar məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _currencyRegulationService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Valyuta tənzimi qaydalar məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Valyuta tənzimi qaydalar məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
