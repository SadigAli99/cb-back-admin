using CB.Application.DTOs.CurrencyHistoryPrev;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.CurrencyHistoryPrev;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class CurrencyHistoryPrevController : ControllerBase
    {
        private readonly ICurrencyHistoryPrevService _currencyHistoryPrevService;
        private readonly CurrencyHistoryPrevCreateValidator _createValidator;
        private readonly CurrencyHistoryPrevEditValidator _editValidator;

        public CurrencyHistoryPrevController(
            ICurrencyHistoryPrevService currencyHistoryPrevService,
            CurrencyHistoryPrevCreateValidator createValidator,
            CurrencyHistoryPrevEditValidator editValidator
        )
        {
            _currencyHistoryPrevService = currencyHistoryPrevService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/currencyHistoryPrev
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _currencyHistoryPrevService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/currencyHistoryPrev/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _currencyHistoryPrevService.GetByIdAsync(id);
            if (data == null)
            {
                Log.Warning("Valyuta tarixçəsi XX əsrdən öncəki məlumatı tapılmadı. Id={Id}", id);
                return NotFound();
            }

            return Ok(data);
        }

        // POST: /api/currencyHistoryPrev
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CurrencyHistoryPrevCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _currencyHistoryPrevService.CreateAsync(dto);

            if (!created)
            {
                Log.Error("Valyuta tarixçXX əsrdən öncəkiəsi əlavə edilməsi uğursuz oldu {@Dto}", dto);
                return BadRequest(new { status = "error", message = "Məlumat əlavə olunmadı" });
            }

            Log.Information("Məlumat uğurla əlavə olundu {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/currencyHistoryPrev/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] CurrencyHistoryPrevEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });

            var updated = await _currencyHistoryPrevService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Error("Valyuta tarixçəsi yeXX əsrdən öncəkinilənməsi uğursuz oldu {@Dto}", dto);
                return NotFound();
            }

            Log.Information("Valyuta tarixçəsi yeXX əsrdən öncəkinilənməsi uğurludur {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        // DELETE: /api/currencyHistoryPrev/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _currencyHistoryPrevService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Valyuta tarixçəsi XX əsrdən öncəkisilinməsi uğursuz oldu Id={@Id}", id);
                return NotFound();
            }
            Log.Information("Valyuta tarixçəXX əsrdən öncəkisi uğurla silindi : Id={@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }
    }
}
