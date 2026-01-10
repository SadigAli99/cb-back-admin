using CB.Application.DTOs.MoneySignProtection;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.MoneySignProtection;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MoneySignProtectionController : ControllerBase
    {
        private readonly IMoneySignProtectionService _moneySignProtectionService;
        private readonly MoneySignProtectionCreateValidator _createValidator;
        private readonly MoneySignProtectionEditValidator _editValidator;

        public MoneySignProtectionController(
            IMoneySignProtectionService moneySignProtectionService,
            MoneySignProtectionCreateValidator createValidator,
            MoneySignProtectionEditValidator editValidator
        )
        {
            _moneySignProtectionService = moneySignProtectionService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<MoneySignProtectionGetDTO> data = await _moneySignProtectionService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            MoneySignProtectionGetDTO? data = await _moneySignProtectionService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Pul nişanı mühafizə məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] MoneySignProtectionCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _moneySignProtectionService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Pul nişanı mühafizə məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Pul nişanı mühafizə məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] MoneySignProtectionEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _moneySignProtectionService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Pul nişanı mühafizə məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Pul nişanı mühafizə məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _moneySignProtectionService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Pul nişanı mühafizə məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Pul nişanı mühafizə məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
