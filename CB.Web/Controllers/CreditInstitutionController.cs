using CB.Application.DTOs.CreditInstitution;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.CreditInstitution;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CreditInstitutionController : ControllerBase
    {
        private readonly ICreditInstitutionService _creditInstitutionService;
        private readonly CreditInstitutionCreateValidator _createValidator;
        private readonly CreditInstitutionEditValidator _editValidator;

        public CreditInstitutionController(
            ICreditInstitutionService creditInstitutionService,
            CreditInstitutionCreateValidator createValidator,
            CreditInstitutionEditValidator editValidator
        )
        {
            _creditInstitutionService = creditInstitutionService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<CreditInstitutionGetDTO> data = await _creditInstitutionService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            CreditInstitutionGetDTO? data = await _creditInstitutionService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Kredit təşkilat məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreditInstitutionCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _creditInstitutionService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Kredit təşkilat məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Kredit təşkilat məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] CreditInstitutionEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _creditInstitutionService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Kredit təşkilat məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Kredit təşkilat məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _creditInstitutionService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Kredit təşkilat məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Kredit təşkilat məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
