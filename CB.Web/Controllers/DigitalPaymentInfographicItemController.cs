using CB.Application.DTOs.DigitalPaymentInfographicItem;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.DigitalPaymentInfographicItem;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class DigitalPaymentInfographicItemController : ControllerBase
    {
        private readonly IDigitalPaymentInfographicItemService _digitalPaymentInfographicItemService;
        private readonly DigitalPaymentInfographicItemCreateValidator _createValidator;
        private readonly DigitalPaymentInfographicItemEditValidator _editValidator;

        public DigitalPaymentInfographicItemController(
            IDigitalPaymentInfographicItemService digitalPaymentInfographicItemService,
            DigitalPaymentInfographicItemCreateValidator createValidator,
            DigitalPaymentInfographicItemEditValidator editValidator
        )
        {
            _digitalPaymentInfographicItemService = digitalPaymentInfographicItemService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/digitalPaymentInfographicItem
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _digitalPaymentInfographicItemService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/digitalPaymentInfographicItem/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _digitalPaymentInfographicItemService.GetByIdAsync(id);
            if (data == null)
            {
                Log.Warning("Rəqəmsal ödəniş infoqrafiyası qrafik məlumatı tapılmadı. Id={Id}", id);
                return NotFound();
            }

            return Ok(data);
        }

        // POST: /api/digitalPaymentInfographicItem
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DigitalPaymentInfographicItemCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _digitalPaymentInfographicItemService.CreateAsync(dto);

            if (!created)
            {
                Log.Error("Rəqəmsal ödəniş infoqrafiyası qrafik əlavə edilməsi uğursuz oldu {@Dto}", dto);
                return BadRequest(new { status = "error", message = "Məlumat əlavə olunmadı" });
            }

            Log.Information("Məlumat uğurla əlavə olundu {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/digitalPaymentInfographicItem/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] DigitalPaymentInfographicItemEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });

            var updated = await _digitalPaymentInfographicItemService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Error("Rəqəmsal ödəniş infoqrafiyası qrafik yenilənməsi uğursuz oldu {@Dto}", dto);
                return NotFound();
            }

            Log.Information("Rəqəmsal ödəniş infoqrafiyası qrafik yenilənməsi uğurludur {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        // DELETE: /api/digitalPaymentInfographicItem/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _digitalPaymentInfographicItemService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Rəqəmsal ödəniş infoqrafiyası qrafik silinməsi uğursuz oldu Id={@Id}", id);
                return NotFound();
            }
            Log.Information("Rəqəmsal ödəniş infoqrafiyası qrafik uğurla silindi : Id={@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }
    }
}
