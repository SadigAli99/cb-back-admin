using CB.Application.DTOs.InfographicDisclosure;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.InfographicDisclosure;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class InfographicDisclosureController : ControllerBase
    {
        private readonly IInfographicDisclosureService _infographicDisclosureService;
        private readonly InfographicDisclosureCreateValidator _createValidator;
        private readonly InfographicDisclosureEditValidator _editValidator;

        public InfographicDisclosureController(
            IInfographicDisclosureService infographicDisclosureService,
            InfographicDisclosureCreateValidator createValidator,
            InfographicDisclosureEditValidator editValidator
        )
        {
            _infographicDisclosureService = infographicDisclosureService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/infographicDisclosure
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _infographicDisclosureService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/infographicDisclosure/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _infographicDisclosureService.GetByIdAsync(id);
            if (data == null)
            {
                Log.Warning("Məlumatların yayımlanması infoqrafiyası məlumatı tapılmadı. Id={Id}", id);
                return NotFound();
            }

            return Ok(data);
        }

        // POST: /api/infographicDisclosure
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InfographicDisclosureCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _infographicDisclosureService.CreateAsync(dto);

            if (!created)
            {
                Log.Error("Məlumatların yayımlanması infoqrafiyası əlavə edilməsi uğursuz oldu {@Dto}", dto);
                return BadRequest(new { status = "error", message = "Məlumat əlavə olunmadı" });
            }

            Log.Information("Məlumat uğurla əlavə olundu {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/infographicDisclosure/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] InfographicDisclosureEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });

            var updated = await _infographicDisclosureService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Error("Məlumatların yayımlanması infoqrafiyası yenilənməsi uğursuz oldu {@Dto}", dto);
                return NotFound();
            }

            Log.Information("Məlumatların yayımlanması infoqrafiyası yenilənməsi uğurludur {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        // DELETE: /api/infographicDisclosure/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _infographicDisclosureService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Məlumatların yayımlanması infoqrafiyası silinməsi uğursuz oldu Id={@Id}", id);
                return NotFound();
            }
            Log.Information("Məlumatların yayımlanması infoqrafiyası uğurla silindi : Id={@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }
    }
}
