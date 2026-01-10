using CB.Application.DTOs.StockExchangeFile;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.StockExchangeFile;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class StockExchangeFileController : ControllerBase
    {
        private readonly IStockExchangeFileService _stockExchangeFileService;
        private readonly StockExchangeFileCreateValidator _createValidator;
        private readonly StockExchangeFileEditValidator _editValidator;

        public StockExchangeFileController(
            IStockExchangeFileService stockExchangeFileService,
            StockExchangeFileCreateValidator createValidator,
            StockExchangeFileEditValidator editValidator
        )
        {
            _stockExchangeFileService = stockExchangeFileService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<StockExchangeFileGetDTO> data = await _stockExchangeFileService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            StockExchangeFileGetDTO? data = await _stockExchangeFileService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("StockExchange fayl məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] StockExchangeFileCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _stockExchangeFileService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("StockExchange fayl məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("StockExchange fayl məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] StockExchangeFileEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _stockExchangeFileService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("StockExchange fayl məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("StockExchange fayl məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _stockExchangeFileService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("StockExchange fayl məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("StockExchange fayl məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
