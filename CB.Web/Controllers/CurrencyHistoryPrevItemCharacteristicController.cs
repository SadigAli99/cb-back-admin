using CB.Application.DTOs.CurrencyHistoryPrevItemCharacteristic;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.CurrencyHistoryPrevItemCharacteristic;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CurrencyHistoryPrevItemCharacteristicController : ControllerBase
    {
        private readonly ICurrencyHistoryPrevItemCharacteristicService _currencyHistoryPrevItemCharacteristicService;
        private readonly CurrencyHistoryPrevItemCharacteristicCreateValidator _createValidator;
        private readonly CurrencyHistoryPrevItemCharacteristicEditValidator _editValidator;

        public CurrencyHistoryPrevItemCharacteristicController(
            ICurrencyHistoryPrevItemCharacteristicService currencyHistoryPrevItemCharacteristicService,
            CurrencyHistoryPrevItemCharacteristicCreateValidator createValidator,
            CurrencyHistoryPrevItemCharacteristicEditValidator editValidator
        )
        {
            _currencyHistoryPrevItemCharacteristicService = currencyHistoryPrevItemCharacteristicService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<CurrencyHistoryPrevItemCharacteristicGetDTO> data = await _currencyHistoryPrevItemCharacteristicService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            CurrencyHistoryPrevItemCharacteristicGetDTO? data = await _currencyHistoryPrevItemCharacteristicService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Valyuta tarixçəsi xarakteristika məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CurrencyHistoryPrevItemCharacteristicCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _currencyHistoryPrevItemCharacteristicService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Valyuta tarixçəsi xarakteristika məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Valyuta tarixçəsi xarakteristika məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] CurrencyHistoryPrevItemCharacteristicEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _currencyHistoryPrevItemCharacteristicService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Valyuta tarixçəsi xarakteristika məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Valyuta tarixçəsi xarakteristika məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _currencyHistoryPrevItemCharacteristicService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Valyuta tarixçəsi xarakteristika məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Valyuta tarixçəsi xarakteristika məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
