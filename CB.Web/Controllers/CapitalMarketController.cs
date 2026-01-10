using CB.Application.DTOs.CapitalMarket;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.CapitalMarket;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CapitalMarketController : ControllerBase
    {
        private readonly ICapitalMarketService _capitalMarketService;
        private readonly CapitalMarketCreateValidator _createValidator;
        private readonly CapitalMarketEditValidator _editValidator;

        public CapitalMarketController(
            ICapitalMarketService capitalMarketService,
            CapitalMarketCreateValidator createValidator,
            CapitalMarketEditValidator editValidator
        )
        {
            _capitalMarketService = capitalMarketService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<CapitalMarketGetDTO> data = await _capitalMarketService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            CapitalMarketGetDTO? data = await _capitalMarketService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Kapital bazarı məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CapitalMarketCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _capitalMarketService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Kapital bazarı məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Kapital bazarı məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] CapitalMarketEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _capitalMarketService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Kapital bazarı məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Kapital bazarı məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _capitalMarketService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Kapital bazarı məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Kapital bazarı məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
