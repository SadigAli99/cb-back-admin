using CB.Application.DTOs.MacroEconomy;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.MacroEconomy;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MacroEconomyController : ControllerBase
    {
        private readonly IMacroEconomyService _macroEconomyService;
        private readonly MacroEconomyCreateValidator _createValidator;
        private readonly MacroEconomyEditValidator _editValidator;

        public MacroEconomyController(
            IMacroEconomyService macroEconomyService,
            MacroEconomyCreateValidator createValidator,
            MacroEconomyEditValidator editValidator
        )
        {
            _macroEconomyService = macroEconomyService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<MacroEconomyGetDTO> data = await _macroEconomyService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            MacroEconomyGetDTO? data = await _macroEconomyService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Makroiqtisadi göstərici məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] MacroEconomyCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _macroEconomyService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Makroiqtisadi göstərici məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Makroiqtisadi göstərici məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] MacroEconomyEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _macroEconomyService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Makroiqtisadi göstərici məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Makroiqtisadi göstərici məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _macroEconomyService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Makroiqtisadi göstərici məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Makroiqtisadi göstərici məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
