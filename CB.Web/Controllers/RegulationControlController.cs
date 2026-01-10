using CB.Application.DTOs.RegulationControl;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.RegulationControl;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RegulationControlController : ControllerBase
    {
        private readonly IRegulationControlService _regulationControlService;
        private readonly RegulationControlCreateValidator _createValidator;
        private readonly RegulationControlEditValidator _editValidator;

        public RegulationControlController(
            IRegulationControlService regulationControlService,
            RegulationControlCreateValidator createValidator,
            RegulationControlEditValidator editValidator
        )
        {
            _regulationControlService = regulationControlService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RegulationControlGetDTO> data = await _regulationControlService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            RegulationControlGetDTO? data = await _regulationControlService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Tənzimləmələr və nəzarət məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] RegulationControlCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _regulationControlService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Tənzimləmələr və nəzarət məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Tənzimləmələr və nəzarət məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] RegulationControlEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _regulationControlService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Tənzimləmələr və nəzarət məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Tənzimləmələr və nəzarət məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _regulationControlService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Tənzimləmələr və nəzarət məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Tənzimləmələr və nəzarət məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
