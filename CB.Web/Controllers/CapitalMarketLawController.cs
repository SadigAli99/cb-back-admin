using CB.Application.DTOs.CapitalMarketLaw;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.CapitalMarketLaw;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CapitalMarketLawController : ControllerBase
    {
        private readonly ICapitalMarketLawService _capitalMarketLawService;
        private readonly CapitalMarketLawCreateValidator _createValidator;
        private readonly CapitalMarketLawEditValidator _editValidator;

        public CapitalMarketLawController(
            ICapitalMarketLawService capitalMarketLawService,
            CapitalMarketLawCreateValidator createValidator,
            CapitalMarketLawEditValidator editValidator
        )
        {
            _capitalMarketLawService = capitalMarketLawService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<CapitalMarketLawGetDTO> data = await _capitalMarketLawService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            CapitalMarketLawGetDTO? data = await _capitalMarketLawService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Kapital bazarı qanunlar məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CapitalMarketLawCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _capitalMarketLawService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Kapital bazarı qanunlar məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Kapital bazarı qanunlar məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] CapitalMarketLawEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _capitalMarketLawService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Kapital bazarı qanunlar məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Kapital bazarı qanunlar məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _capitalMarketLawService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Kapital bazarı qanunlar məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Kapital bazarı qanunlar məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
