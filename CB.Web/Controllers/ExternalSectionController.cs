using CB.Application.DTOs.ExternalSection;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.ExternalSection;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ExternalSectionController : ControllerBase
    {
        private readonly IExternalSectionService _externalSectionService;
        private readonly ExternalSectionCreateValidator _createValidator;
        private readonly ExternalSectionEditValidator _editValidator;

        public ExternalSectionController(
            IExternalSectionService externalSectionService,
            ExternalSectionCreateValidator createValidator,
            ExternalSectionEditValidator editValidator
        )
        {
            _externalSectionService = externalSectionService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<ExternalSectionGetDTO> data = await _externalSectionService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            ExternalSectionGetDTO? data = await _externalSectionService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Xarici sektor məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ExternalSectionCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _externalSectionService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Xarici sektor məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Xarici sektor məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] ExternalSectionEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _externalSectionService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Xarici sektor məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Xarici sektor məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _externalSectionService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Xarici sektor məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Xarici sektor məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
