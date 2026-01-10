using CB.Application.DTOs.OutOfCoinMoneySignHistory;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.OutOfCoinMoneySignHistory;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OutOfCoinMoneySignHistoryController : ControllerBase
    {
        private readonly IOutOfCoinMoneySignHistoryService _moneySignHistoryService;
        private readonly OutOfCoinMoneySignHistoryCreateValidator _createValidator;
        private readonly OutOfCoinMoneySignHistoryEditValidator _editValidator;

        public OutOfCoinMoneySignHistoryController(
            IOutOfCoinMoneySignHistoryService moneySignHistoryService,
            OutOfCoinMoneySignHistoryCreateValidator createValidator,
            OutOfCoinMoneySignHistoryEditValidator editValidator
        )
        {
            _moneySignHistoryService = moneySignHistoryService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<OutOfCoinMoneySignHistoryGetDTO> data = await _moneySignHistoryService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            OutOfCoinMoneySignHistoryGetDTO? data = await _moneySignHistoryService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Tədavüldən kənar metal pul nişanı tarixçəsi məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OutOfCoinMoneySignHistoryCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _moneySignHistoryService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Tədavüldən kənar metal pul nişanı tarixçəsi məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Tədavüldən kənar metal pul nişanı tarixçəsi məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] OutOfCoinMoneySignHistoryEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _moneySignHistoryService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Tədavüldən kənar metal pul nişanı tarixçəsi məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Tədavüldən kənar metal pul nişanı tarixçəsi məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _moneySignHistoryService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Tədavüldən kənar metal pul nişanı tarixçəsi məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Tədavüldən kənar metal pul nişanı tarixçəsi məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
