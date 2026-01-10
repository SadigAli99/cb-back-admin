using CB.Application.DTOs.Software;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.Software;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SoftwareController : ControllerBase
    {
        private readonly ISoftwareService _softwareService;
        private readonly SoftwareCreateValidator _createValidator;
        private readonly SoftwareEditValidator _editValidator;

        public SoftwareController(
            ISoftwareService softwareService,
            SoftwareCreateValidator createValidator,
            SoftwareEditValidator editValidator
        )
        {
            _softwareService = softwareService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<SoftwareGetDTO> data = await _softwareService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            SoftwareGetDTO? data = await _softwareService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Proqram təminatı məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] SoftwareCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _softwareService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Proqram təminatı məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Proqram təminatı məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] SoftwareEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _softwareService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Proqram təminatı məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Proqram təminatı məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _softwareService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Proqram təminatı məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Proqram təminatı məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
