using CB.Application.DTOs.DigitalPaymentInfographic;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.DigitalPaymentInfographic;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class DigitalPaymentInfographicController : ControllerBase
    {
        private readonly IDigitalPaymentInfographicService _digitalPaymentInfographicService;
        private readonly DigitalPaymentInfographicCreateValidator _createValidator;
        private readonly DigitalPaymentInfographicEditValidator _editValidator;

        public DigitalPaymentInfographicController(
            IDigitalPaymentInfographicService digitalPaymentInfographicService,
            DigitalPaymentInfographicCreateValidator createValidator,
            DigitalPaymentInfographicEditValidator editValidator
        )
        {
            _digitalPaymentInfographicService = digitalPaymentInfographicService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/digitalPaymentInfographic
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _digitalPaymentInfographicService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/digitalPaymentInfographic/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _digitalPaymentInfographicService.GetByIdAsync(id);
            if (data == null)
            {
                Log.Warning("Rəqəmsal ödəniş infoqrafiyası məlumatı tapılmadı. Id={Id}", id);
                return NotFound();
            }

            return Ok(data);
        }

        // POST: /api/digitalPaymentInfographic
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DigitalPaymentInfographicCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _digitalPaymentInfographicService.CreateAsync(dto);

            if (!created)
            {
                Log.Error("Rəqəmsal ödəniş infoqrafiyası əlavə edilməsi uğursuz oldu {@Dto}", dto);
                return BadRequest(new { status = "error", message = "Məlumat əlavə olunmadı" });
            }

            Log.Information("Məlumat uğurla əlavə olundu {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/digitalPaymentInfographic/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] DigitalPaymentInfographicEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });

            var updated = await _digitalPaymentInfographicService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Error("Rəqəmsal ödəniş infoqrafiyası yenilənməsi uğursuz oldu {@Dto}", dto);
                return NotFound();
            }

            Log.Information("Rəqəmsal ödəniş infoqrafiyası yenilənməsi uğurludur {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        // DELETE: /api/digitalPaymentInfographic/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _digitalPaymentInfographicService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Rəqəmsal ödəniş infoqrafiyası silinməsi uğursuz oldu Id={@Id}", id);
                return NotFound();
            }
            Log.Information("Rəqəmsal ödəniş infoqrafiyası uğurla silindi : Id={@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }
    }
}
