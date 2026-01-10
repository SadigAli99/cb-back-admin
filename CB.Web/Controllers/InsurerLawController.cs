using CB.Application.DTOs.InsurerLaw;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.InsurerLaw;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InsurerLawController : ControllerBase
    {
        private readonly IInsurerLawService _insurerLawService;
        private readonly InsurerLawCreateValidator _createValidator;
        private readonly InsurerLawEditValidator _editValidator;

        public InsurerLawController(
            IInsurerLawService insurerLawService,
            InsurerLawCreateValidator createValidator,
            InsurerLawEditValidator editValidator
        )
        {
            _insurerLawService = insurerLawService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<InsurerLawGetDTO> data = await _insurerLawService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            InsurerLawGetDTO? data = await _insurerLawService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Sığortaçı qanunlar məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] InsurerLawCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _insurerLawService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Sığortaçı qanunlar məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Sığortaçı qanunlar məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] InsurerLawEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _insurerLawService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Sığortaçı qanunlar məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Sığortaçı qanunlar məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _insurerLawService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Sığortaçı qanunlar məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Sığortaçı qanunlar məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
