using CB.Application.DTOs.CreditInstitutionLaw;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.CreditInstitutionLaw;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CreditInstitutionLawController : ControllerBase
    {
        private readonly ICreditInstitutionLawService _creditInstitutionLawService;
        private readonly CreditInstitutionLawCreateValidator _createValidator;
        private readonly CreditInstitutionLawEditValidator _editValidator;

        public CreditInstitutionLawController(
            ICreditInstitutionLawService creditInstitutionLawService,
            CreditInstitutionLawCreateValidator createValidator,
            CreditInstitutionLawEditValidator editValidator
        )
        {
            _creditInstitutionLawService = creditInstitutionLawService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<CreditInstitutionLawGetDTO> data = await _creditInstitutionLawService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            CreditInstitutionLawGetDTO? data = await _creditInstitutionLawService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Kredit təşkilatı qanunlar məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreditInstitutionLawCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _creditInstitutionLawService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Kredit təşkilatı qanunlar məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Kredit təşkilatı qanunlar məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] CreditInstitutionLawEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _creditInstitutionLawService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Kredit təşkilatı qanunlar məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Kredit təşkilatı qanunlar məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _creditInstitutionLawService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Kredit təşkilatı qanunlar məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Kredit təşkilatı qanunlar məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
