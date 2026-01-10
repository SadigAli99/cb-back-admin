using CB.Application.DTOs.CreditInstitutionPresidentAct;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.CreditInstitutionPresidentAct;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CreditInstitutionPresidentActController : ControllerBase
    {
        private readonly ICreditInstitutionPresidentActService _creditInstitutionPresidentActService;
        private readonly CreditInstitutionPresidentActCreateValidator _createValidator;
        private readonly CreditInstitutionPresidentActEditValidator _editValidator;

        public CreditInstitutionPresidentActController(
            ICreditInstitutionPresidentActService creditInstitutionPresidentActService,
            CreditInstitutionPresidentActCreateValidator createValidator,
            CreditInstitutionPresidentActEditValidator editValidator
        )
        {
            _creditInstitutionPresidentActService = creditInstitutionPresidentActService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<CreditInstitutionPresidentActGetDTO> data = await _creditInstitutionPresidentActService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            CreditInstitutionPresidentActGetDTO? data = await _creditInstitutionPresidentActService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Kredit təşkilatı AR prezidentinin aktları məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreditInstitutionPresidentActCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _creditInstitutionPresidentActService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Kredit təşkilatı AR prezidentinin aktları məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Kredit təşkilatı AR prezidentinin aktları məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] CreditInstitutionPresidentActEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _creditInstitutionPresidentActService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Kredit təşkilatı AR prezidentinin aktları məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Kredit təşkilatı AR prezidentinin aktları məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _creditInstitutionPresidentActService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Kredit təşkilatı AR prezidentinin aktları məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Kredit təşkilatı AR prezidentinin aktları məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
