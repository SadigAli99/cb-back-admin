using CB.Application.DTOs.LegalBasis;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.LegalBasis;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LegalBasisController : ControllerBase
    {
        private readonly ILegalBasisService _legalBasisService;
        private readonly LegalBasisCreateValidator _createValidator;
        private readonly LegalBasisEditValidator _editValidator;

        public LegalBasisController(
            ILegalBasisService legalBasisService,
            LegalBasisCreateValidator createValidator,
            LegalBasisEditValidator editValidator
        )
        {
            _legalBasisService = legalBasisService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<LegalBasisGetDTO> data = await _legalBasisService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            LegalBasisGetDTO? data = await _legalBasisService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Hüquqi əsaslar məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] LegalBasisCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _legalBasisService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Hüquqi əsaslar məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Hüquqi əsaslar məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] LegalBasisEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _legalBasisService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Hüquqi əsaslar məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Hüquqi əsaslar məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _legalBasisService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Hüquqi əsaslar məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Hüquqi əsaslar məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
