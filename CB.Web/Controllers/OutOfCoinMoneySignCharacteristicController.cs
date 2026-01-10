using CB.Application.DTOs.OutOfCoinMoneySignCharacteristic;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.OutOfCoinMoneySignCharacteristic;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OutOfCoinMoneySignCharacteristicController : ControllerBase
    {
        private readonly IOutOfCoinMoneySignCharacteristicService _moneySignCharacteristicService;
        private readonly OutOfCoinMoneySignCharacteristicCreateValidator _createValidator;
        private readonly OutOfCoinMoneySignCharacteristicEditValidator _editValidator;

        public OutOfCoinMoneySignCharacteristicController(
            IOutOfCoinMoneySignCharacteristicService moneySignCharacteristicService,
            OutOfCoinMoneySignCharacteristicCreateValidator createValidator,
            OutOfCoinMoneySignCharacteristicEditValidator editValidator
        )
        {
            _moneySignCharacteristicService = moneySignCharacteristicService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<OutOfCoinMoneySignCharacteristicGetDTO> data = await _moneySignCharacteristicService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            OutOfCoinMoneySignCharacteristicGetDTO? data = await _moneySignCharacteristicService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Tədavüldən kənar metal pul nişanı xarakteristika məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OutOfCoinMoneySignCharacteristicCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _moneySignCharacteristicService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Tədavüldən kənar metal pul nişanı xarakteristika məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Tədavüldən kənar metal pul nişanı xarakteristika məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] OutOfCoinMoneySignCharacteristicEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _moneySignCharacteristicService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Tədavüldən kənar metal pul nişanı xarakteristika məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Tədavüldən kənar metal pul nişanı xarakteristika məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _moneySignCharacteristicService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Tədavüldən kənar metal pul nişanı xarakteristika məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Tədavüldən kənar metal pul nişanı xarakteristika məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
