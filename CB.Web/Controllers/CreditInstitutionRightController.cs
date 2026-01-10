using CB.Application.DTOs.CreditInstitutionRight;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.CreditInstitutionRight;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CreditInstitutionRightController : ControllerBase
    {
        private readonly ICreditInstitutionRightService _creditInstitutionRightService;
        private readonly CreditInstitutionRightCreateValidator _createValidator;
        private readonly CreditInstitutionRightEditValidator _editValidator;

        public CreditInstitutionRightController(
            ICreditInstitutionRightService creditInstitutionRightService,
            CreditInstitutionRightCreateValidator createValidator,
            CreditInstitutionRightEditValidator editValidator
        )
        {
            _creditInstitutionRightService = creditInstitutionRightService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<CreditInstitutionRightGetDTO> data = await _creditInstitutionRightService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            CreditInstitutionRightGetDTO? data = await _creditInstitutionRightService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Kredit təşkilatı qaydalar məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreditInstitutionRightCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _creditInstitutionRightService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Kredit təşkilatı qaydalar məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Kredit təşkilatı qaydalar məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] CreditInstitutionRightEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _creditInstitutionRightService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Kredit təşkilatı qaydalar məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Kredit təşkilatı qaydalar məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _creditInstitutionRightService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Kredit təşkilatı qaydalar məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Kredit təşkilatı qaydalar məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
