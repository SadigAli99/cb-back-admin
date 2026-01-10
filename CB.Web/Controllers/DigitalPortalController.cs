using CB.Application.DTOs.DigitalPortal;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.DigitalPortal;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DigitalPortalController : ControllerBase
    {
        private readonly IDigitalPortalService _digitalPortalService;
        private readonly DigitalPortalCreateValidator _createValidator;
        private readonly DigitalPortalEditValidator _editValidator;

        public DigitalPortalController(
            IDigitalPortalService digitalPortalService,
            DigitalPortalCreateValidator createValidator,
            DigitalPortalEditValidator editValidator
        )
        {
            _digitalPortalService = digitalPortalService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<DigitalPortalGetDTO> data = await _digitalPortalService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            DigitalPortalGetDTO? data = await _digitalPortalService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Rəqəmsal portal məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] DigitalPortalCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _digitalPortalService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Rəqəmsal portal məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Rəqəmsal portal məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] DigitalPortalEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _digitalPortalService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Rəqəmsal portal məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Rəqəmsal portal məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _digitalPortalService.DeleteAsync(id);

            if (!deleted)
            {
                Log.Warning("Rəqəmsal portal məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Rəqəmsal portal məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });


        }

    }
}
