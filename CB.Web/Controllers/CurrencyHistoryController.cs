using CB.Application.DTOs.CurrencyHistory;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.CurrencyHistory;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class CurrencyHistoryController : ControllerBase
    {
        private readonly ICurrencyHistoryService _currencyHistoryService;
        private readonly CurrencyHistoryCreateValidator _createValidator;
        private readonly CurrencyHistoryEditValidator _editValidator;

        public CurrencyHistoryController(
            ICurrencyHistoryService currencyHistoryService,
            CurrencyHistoryCreateValidator createValidator,
            CurrencyHistoryEditValidator editValidator
        )
        {
            _currencyHistoryService = currencyHistoryService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/currencyHistory
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _currencyHistoryService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/currencyHistory/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _currencyHistoryService.GetByIdAsync(id);
            if (data == null)
            {
                Log.Warning("Valyuta tarixçəsi məlumatı tapılmadı. Id={Id}", id);
                return NotFound();
            }

            return Ok(data);
        }

        // POST: /api/currencyHistory
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CurrencyHistoryCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _currencyHistoryService.CreateAsync(dto);

            if (!created)
            {
                Log.Error("Valyuta tarixçəsi əlavə edilməsi uğursuz oldu {@Dto}", dto);
                return BadRequest(new { status = "error", message = "Məlumat əlavə olunmadı" });
            }

            Log.Information("Məlumat uğurla əlavə olundu {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/currencyHistory/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] CurrencyHistoryEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });

            var updated = await _currencyHistoryService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Error("Valyuta tarixçəsi yenilənməsi uğursuz oldu {@Dto}", dto);
                return NotFound();
            }

            Log.Information("Valyuta tarixçəsi yenilənməsi uğurludur {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        // DELETE: /api/currencyHistory/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _currencyHistoryService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Valyuta tarixçəsi silinməsi uğursuz oldu Id={@Id}", id);
                return NotFound();
            }
            Log.Information("Valyuta tarixçəsi uğurla silindi : Id={@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }
    }
}
