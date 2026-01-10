using CB.Application.DTOs.InsurerRight;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.InsurerRight;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InsurerRightController : ControllerBase
    {
        private readonly IInsurerRightService _insurerRightService;
        private readonly InsurerRightCreateValidator _createValidator;
        private readonly InsurerRightEditValidator _editValidator;

        public InsurerRightController(
            IInsurerRightService insurerRightService,
            InsurerRightCreateValidator createValidator,
            InsurerRightEditValidator editValidator
        )
        {
            _insurerRightService = insurerRightService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<InsurerRightGetDTO> data = await _insurerRightService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            InsurerRightGetDTO? data = await _insurerRightService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Sığortaçı qaydalar məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] InsurerRightCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _insurerRightService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Sığortaçı qaydalar məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Sığortaçı qaydalar məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] InsurerRightEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _insurerRightService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Sığortaçı qaydalar məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Sığortaçı qaydalar məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _insurerRightService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Sığortaçı qaydalar məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Sığortaçı qaydalar məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
