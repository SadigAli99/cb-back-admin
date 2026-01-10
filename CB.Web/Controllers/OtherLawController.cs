using CB.Application.DTOs.OtherLaw;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.OtherLaw;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OtherLawController : ControllerBase
    {
        private readonly IOtherLawService _otherLawService;
        private readonly OtherLawCreateValidator _createValidator;
        private readonly OtherLawEditValidator _editValidator;

        public OtherLawController(
            IOtherLawService otherLawService,
            OtherLawCreateValidator createValidator,
            OtherLawEditValidator editValidator
        )
        {
            _otherLawService = otherLawService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<OtherLawGetDTO> data = await _otherLawService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            OtherLawGetDTO? data = await _otherLawService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Digər qanunlar məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] OtherLawCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _otherLawService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Digər qanunlar məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Digər qanunlar məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] OtherLawEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _otherLawService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Digər qanunlar məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Digər qanunlar məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _otherLawService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Digər qanunlar məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Digər qanunlar məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
