using CB.Application.DTOs.InfographicDisclosureFrequency;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.InfographicDisclosureFrequency;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class InfographicDisclosureFrequencyController : ControllerBase
    {
        private readonly IInfographicDisclosureFrequencyService _infographicDisclosureFrequencyService;
        private readonly InfographicDisclosureFrequencyCreateValidator _createValidator;
        private readonly InfographicDisclosureFrequencyEditValidator _editValidator;

        public InfographicDisclosureFrequencyController(
            IInfographicDisclosureFrequencyService infographicDisclosureFrequencyService,
            InfographicDisclosureFrequencyCreateValidator createValidator,
            InfographicDisclosureFrequencyEditValidator editValidator
        )
        {
            _infographicDisclosureFrequencyService = infographicDisclosureFrequencyService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/infographicDisclosureFrequency
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _infographicDisclosureFrequencyService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/infographicDisclosureFrequency/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _infographicDisclosureFrequencyService.GetByIdAsync(id);
            if (data == null)
            {
                Log.Warning("Məlumatların yayımlanması infoqrafiyası dövrilik məlumatı tapılmadı. Id={Id}", id);
                return NotFound();
            }

            return Ok(data);
        }

        // POST: /api/infographicDisclosureFrequency
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InfographicDisclosureFrequencyCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _infographicDisclosureFrequencyService.CreateAsync(dto);

            if (!created)
            {
                Log.Error("Məlumatların yayımlanması infoqrafiyası dövrilik əlavə edilməsi uğursuz oldu {@Dto}", dto);
                return BadRequest(new { status = "error", message = "Məlumat əlavə olunmadı" });
            }

            Log.Information("Məlumat uğurla əlavə olundu {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/infographicDisclosureFrequency/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] InfographicDisclosureFrequencyEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });

            var updated = await _infographicDisclosureFrequencyService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Error("Məlumatların yayımlanması infoqrafiyası dövrilik yenilənməsi uğursuz oldu {@Dto}", dto);
                return NotFound();
            }

            Log.Information("Məlumatların yayımlanması infoqrafiyası dövrilik yenilənməsi uğurludur {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        // DELETE: /api/infographicDisclosureFrequency/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _infographicDisclosureFrequencyService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Məlumatların yayımlanması infoqrafiyası dövrilik silinməsi uğursuz oldu Id={@Id}", id);
                return NotFound();
            }
            Log.Information("Məlumatların yayımlanması infoqrafiyası dövrilik uğurla silindi : Id={@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }
    }
}
