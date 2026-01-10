using CB.Application.DTOs.CapitalMarketRight;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.CapitalMarketRight;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CapitalMarketRightController : ControllerBase
    {
        private readonly ICapitalMarketRightService _capitalMarketRightService;
        private readonly CapitalMarketRightCreateValidator _createValidator;
        private readonly CapitalMarketRightEditValidator _editValidator;

        public CapitalMarketRightController(
            ICapitalMarketRightService capitalMarketRightService,
            CapitalMarketRightCreateValidator createValidator,
            CapitalMarketRightEditValidator editValidator
        )
        {
            _capitalMarketRightService = capitalMarketRightService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<CapitalMarketRightGetDTO> data = await _capitalMarketRightService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            CapitalMarketRightGetDTO? data = await _capitalMarketRightService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Kapital bazarı qaydalar məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CapitalMarketRightCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _capitalMarketRightService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Kapital bazarı qaydalar məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Kapital bazarı qaydalar məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] CapitalMarketRightEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _capitalMarketRightService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Kapital bazarı qaydalar məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Kapital bazarı qaydalar məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _capitalMarketRightService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Kapital bazarı qaydalar məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Kapital bazarı qaydalar məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
