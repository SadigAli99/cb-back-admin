using CB.Application.DTOs.MoneySignProtectionElement;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.MoneySignProtectionElement;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MoneySignProtectionElementController : ControllerBase
    {
        private readonly IMoneySignProtectionElementService _moneySignProtectionElementService;
        private readonly MoneySignProtectionElementCreateValidator _createValidator;
        private readonly MoneySignProtectionElementEditValidator _editValidator;

        public MoneySignProtectionElementController(
            IMoneySignProtectionElementService moneySignProtectionElementService,
            MoneySignProtectionElementCreateValidator createValidator,
            MoneySignProtectionElementEditValidator editValidator
        )
        {
            _moneySignProtectionElementService = moneySignProtectionElementService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int? id)
        {
            List<MoneySignProtectionElementGetDTO> data = await _moneySignProtectionElementService.GetAllAsync(id);
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            MoneySignProtectionElementGetDTO? data = await _moneySignProtectionElementService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Pul nişanı mühafizə elementi məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] MoneySignProtectionElementCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _moneySignProtectionElementService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Pul nişanı mühafizə elementi məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Pul nişanı mühafizə elementi məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] MoneySignProtectionElementEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _moneySignProtectionElementService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Pul nişanı mühafizə elementi məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Pul nişanı mühafizə elementi məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _moneySignProtectionElementService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Pul nişanı mühafizə elementi məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Pul nişanı mühafizə elementi məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
