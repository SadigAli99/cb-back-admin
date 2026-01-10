using CB.Application.DTOs.Chronology;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.Chronology;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ChronologyController : ControllerBase
    {
        private readonly IChronologyService _chronologyService;
        private readonly ChronologyCreateValidator _createValidator;
        private readonly ChronologyEditValidator _editValidator;

        public ChronologyController(
            IChronologyService chronologyService,
            ChronologyCreateValidator createValidator,
            ChronologyEditValidator editValidator
        )
        {
            _chronologyService = chronologyService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<ChronologyGetDTO> data = await _chronologyService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            ChronologyGetDTO? data = await _chronologyService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Xronologiya məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ChronologyCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _chronologyService.CreateAsync(dTO);

            if (!created)
            {
                Log.Warning("Xronologiya məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Xronologiya məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] ChronologyEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _chronologyService.UpdateAsync(id, dTO);

            if (!updated)
            {
                Log.Warning("Xronologiya məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Xronologiya məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _chronologyService.DeleteAsync(id);

            if (!deleted)
            {
                Log.Warning("Xronologiya məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Xronologiya məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
