using CB.Application.DTOs.LegalAct;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.LegalAct;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LegalActController : ControllerBase
    {
        private readonly ILegalActService _legalActService;
        private readonly LegalActCreateValidator _createValidator;
        private readonly LegalActEditValidator _editValidator;

        public LegalActController(
            ILegalActService legalActService,
            LegalActCreateValidator createValidator,
            LegalActEditValidator editValidator
        )
        {
            _legalActService = legalActService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<LegalActGetDTO> data = await _legalActService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            LegalActGetDTO? data = await _legalActService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Hüquqi akt məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] LegalActCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _legalActService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Hüquqi akt məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Hüquqi akt məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] LegalActEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _legalActService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Hüquqi akt məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Hüquqi akt məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _legalActService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Hüquqi akt məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Hüquqi akt məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
