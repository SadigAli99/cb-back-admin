using CB.Application.DTOs.CreditInstitutionMinisterAct;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.CreditInstitutionMinisterAct;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CreditInstitutionMinisterActController : ControllerBase
    {
        private readonly ICreditInstitutionMinisterActService _creditInstitutionMinisterActService;
        private readonly CreditInstitutionMinisterActCreateValidator _createValidator;
        private readonly CreditInstitutionMinisterActEditValidator _editValidator;

        public CreditInstitutionMinisterActController(
            ICreditInstitutionMinisterActService creditInstitutionMinisterActService,
            CreditInstitutionMinisterActCreateValidator createValidator,
            CreditInstitutionMinisterActEditValidator editValidator
        )
        {
            _creditInstitutionMinisterActService = creditInstitutionMinisterActService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<CreditInstitutionMinisterActGetDTO> data = await _creditInstitutionMinisterActService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            CreditInstitutionMinisterActGetDTO? data = await _creditInstitutionMinisterActService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Kredit təşkilatı Nazirlər Kabinetinin aktları məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreditInstitutionMinisterActCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _creditInstitutionMinisterActService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Kredit təşkilatı Nazirlər Kabinetinin aktları məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Kredit təşkilatı Nazirlər Kabinetinin aktları məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] CreditInstitutionMinisterActEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _creditInstitutionMinisterActService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Kredit təşkilatı Nazirlər Kabinetinin aktları məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Kredit təşkilatı Nazirlər Kabinetinin aktları məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _creditInstitutionMinisterActService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Kredit təşkilatı Nazirlər Kabinetinin aktları məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Kredit təşkilatı Nazirlər Kabinetinin aktları məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
