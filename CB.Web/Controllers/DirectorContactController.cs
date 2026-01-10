using CB.Application.DTOs.DirectorContact;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.DirectorContact;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DirectorContactController : ControllerBase
    {
        private readonly IDirectorContactService _directorContactService;
        private readonly DirectorContactCreateValidator _createValidator;
        private readonly DirectorContactEditValidator _editValidator;

        public DirectorContactController(
            IDirectorContactService directorContactService,
            DirectorContactCreateValidator createValidator,
            DirectorContactEditValidator editValidator
        )
        {
            _directorContactService = directorContactService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int id)
        {
            List<DirectorContactGetDTO> data = await _directorContactService.GetAllAsync(id);
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            DirectorContactGetDTO? data = await _directorContactService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Rəhbər əlaqə məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DirectorContactCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _directorContactService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Rəhbər əlaqə məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Rəhbər əlaqə məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] DirectorContactEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _directorContactService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Rəhbər əlaqə məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Rəhbər əlaqə məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _directorContactService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Rəhbər əlaqə məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Rəhbər əlaqə məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
