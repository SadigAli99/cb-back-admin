using CB.Application.DTOs.CoinMoneySignCharacteristicImage;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.CoinMoneySignCharacteristicImage;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CoinMoneySignCharacteristicImageController : ControllerBase
    {
        private readonly ICoinMoneySignCharacteristicImageService _moneySignCharacteristicImageService;
        private readonly CoinMoneySignCharacteristicImageCreateValidator _createValidator;
        private readonly CoinMoneySignCharacteristicImageEditValidator _editValidator;

        public CoinMoneySignCharacteristicImageController(
            ICoinMoneySignCharacteristicImageService moneySignCharacteristicImageService,
            CoinMoneySignCharacteristicImageCreateValidator createValidator,
            CoinMoneySignCharacteristicImageEditValidator editValidator
        )
        {
            _moneySignCharacteristicImageService = moneySignCharacteristicImageService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<CoinMoneySignCharacteristicImageGetDTO> data = await _moneySignCharacteristicImageService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            CoinMoneySignCharacteristicImageGetDTO? data = await _moneySignCharacteristicImageService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Pul nişanı tarixçəsi şəkil məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CoinMoneySignCharacteristicImageCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _moneySignCharacteristicImageService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Pul nişanı tarixçəsi şəkil məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Pul nişanı tarixçəsi şəkil məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] CoinMoneySignCharacteristicImageEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _moneySignCharacteristicImageService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Pul nişanı tarixçəsi şəkil məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Pul nişanı tarixçəsi şəkil məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _moneySignCharacteristicImageService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Pul nişanı tarixçəsi mə şəkillumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Pul nişanı tarixçəsi şəkil məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
