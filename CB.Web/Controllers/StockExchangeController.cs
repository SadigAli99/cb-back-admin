using CB.Application.DTOs.StockExchange;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.StockExchange;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class StockExchangeController : ControllerBase
    {
        private readonly IStockExchangeService _stockExchangeService;
        private readonly StockExchangeCreateValidator _createValidator;
        private readonly StockExchangeEditValidator _editValidator;

        public StockExchangeController(
            IStockExchangeService stockExchangeService,
            StockExchangeCreateValidator createValidator,
            StockExchangeEditValidator editValidator
        )
        {
            _stockExchangeService = stockExchangeService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<StockExchangeGetDTO> data = await _stockExchangeService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            StockExchangeGetDTO? data = await _stockExchangeService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Stock Exchange məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] StockExchangeCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _stockExchangeService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("StockExchange məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Stock Exchange məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] StockExchangeEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _stockExchangeService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("StockExchange məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Stock Exchange məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _stockExchangeService.DeleteAsync(id);

            if (!deleted)
            {
                Log.Warning("StockExchange məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("StockExchange məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });


        }

    }
}
