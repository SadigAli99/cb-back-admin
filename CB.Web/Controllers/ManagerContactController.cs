using CB.Application.DTOs.ManagerContact;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.ManagerContact;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ManagerContactController : ControllerBase
    {
        private readonly IManagerContactService _managerContactService;
        private readonly ManagerContactCreateValidator _createValidator;
        private readonly ManagerContactEditValidator _editValidator;

        public ManagerContactController(
            IManagerContactService managerContactService,
            ManagerContactCreateValidator createValidator,
            ManagerContactEditValidator editValidator
        )
        {
            _managerContactService = managerContactService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int id)
        {
            List<ManagerContactGetDTO> data = await _managerContactService.GetAllAsync(id);
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            ManagerContactGetDTO? data = await _managerContactService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Rəhbər əlaqə məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ManagerContactCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _managerContactService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Rəhbər əlaqə məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Rəhbər əlaqə məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] ManagerContactEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _managerContactService.UpdateAsync(id, dTO);
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
            var deleted = await _managerContactService.DeleteAsync(id);
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
