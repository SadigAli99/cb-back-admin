using CB.Application.DTOs.CurrencyHistoryNext;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.CurrencyHistoryNext;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class CurrencyHistoryNextController : ControllerBase
    {
        private readonly ICurrencyHistoryNextService _currencyHistoryNextService;
        private readonly CurrencyHistoryNextCreateValidator _createValidator;
        private readonly CurrencyHistoryNextEditValidator _editValidator;

        public CurrencyHistoryNextController(
            ICurrencyHistoryNextService currencyHistoryNextService,
            CurrencyHistoryNextCreateValidator createValidator,
            CurrencyHistoryNextEditValidator editValidator
        )
        {
            _currencyHistoryNextService = currencyHistoryNextService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/currencyHistoryNext
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _currencyHistoryNextService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/currencyHistoryNext/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _currencyHistoryNextService.GetByIdAsync(id);
            if (data == null)
            {
                Log.Warning("Valyuta tarixçəsi XX əsrdən sonrası məlumatı tapılmadı. Id={Id}", id);
                return NotFound();
            }

            return Ok(data);
        }

        // POST: /api/currencyHistoryNext
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CurrencyHistoryNextCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _currencyHistoryNextService.CreateAsync(dto);

            if (!created)
            {
                Log.Error("Valyuta tarixçXX əsrdən sonrası əlavə edilməsi uğursuz oldu {@Dto}", dto);
                return BadRequest(new { status = "error", message = "Məlumat əlavə olunmadı" });
            }

            Log.Information("Məlumat uğurla əlavə olundu {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/currencyHistoryNext/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] CurrencyHistoryNextEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });

            var updated = await _currencyHistoryNextService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Error("Valyuta tarixçəsi yeXX əsrdən sonrasınilənməsi uğursuz oldu {@Dto}", dto);
                return NotFound();
            }

            Log.Information("Valyuta tarixçəsi yeXX əsrdən sonrasınilənməsi uğurludur {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        // DELETE: /api/currencyHistoryNext/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _currencyHistoryNextService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Valyuta tarixçəsi XX əsrdən sonrasısilinməsi uğursuz oldu Id={@Id}", id);
                return NotFound();
            }
            Log.Information("Valyuta tarixçəXX əsrdən sonrasısi uğurla silindi : Id={@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }
    }
}
