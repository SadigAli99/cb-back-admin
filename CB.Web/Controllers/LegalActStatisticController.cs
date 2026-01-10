using CB.Application.DTOs.LegalActStatistic;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.LegalActStatistic;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LegalActStatisticController : ControllerBase
    {
        private readonly ILegalActStatisticService _legalActStatisticService;
        private readonly LegalActStatisticCreateValidator _createValidator;
        private readonly LegalActStatisticEditValidator _editValidator;

        public LegalActStatisticController(
            ILegalActStatisticService legalActStatisticService,
            LegalActStatisticCreateValidator createValidator,
            LegalActStatisticEditValidator editValidator
        )
        {
            _legalActStatisticService = legalActStatisticService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<LegalActStatisticGetDTO> data = await _legalActStatisticService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            LegalActStatisticGetDTO? data = await _legalActStatisticService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Statistika məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] LegalActStatisticCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _legalActStatisticService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Statistika məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Statistika məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] LegalActStatisticEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _legalActStatisticService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Statistika məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Statistika məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _legalActStatisticService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Statistika məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Statistika məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
