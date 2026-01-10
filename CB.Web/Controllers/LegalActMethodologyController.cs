using CB.Application.DTOs.LegalActMethodology;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.LegalActMethodology;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LegalActMethodologyController : ControllerBase
    {
        private readonly ILegalActMethodologyService _legalActMethodologyService;
        private readonly LegalActMethodologyCreateValidator _createValidator;
        private readonly LegalActMethodologyEditValidator _editValidator;

        public LegalActMethodologyController(
            ILegalActMethodologyService legalActMethodologyService,
            LegalActMethodologyCreateValidator createValidator,
            LegalActMethodologyEditValidator editValidator
        )
        {
            _legalActMethodologyService = legalActMethodologyService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<LegalActMethodologyGetDTO> data = await _legalActMethodologyService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            LegalActMethodologyGetDTO? data = await _legalActMethodologyService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Metodologiya məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] LegalActMethodologyCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _legalActMethodologyService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Metodologiya məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Metodologiya məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] LegalActMethodologyEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _legalActMethodologyService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Metodologiya məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Metodologiya məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _legalActMethodologyService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Metodologiya məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Metodologiya məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
