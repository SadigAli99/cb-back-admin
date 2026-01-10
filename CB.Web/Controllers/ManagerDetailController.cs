using CB.Application.DTOs.ManagerDetail;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.ManagerDetail;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ManagerDetailController : ControllerBase
    {
        private readonly IManagerDetailService _managerDetailService;
        private readonly ManagerDetailCreateValidator _createValidator;
        private readonly ManagerDetailEditValidator _editValidator;

        public ManagerDetailController(
            IManagerDetailService managerDetailService,
            ManagerDetailCreateValidator createValidator,
            ManagerDetailEditValidator editValidator
        )
        {
            _managerDetailService = managerDetailService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int id)
        {
            List<ManagerDetailGetDTO> data = await _managerDetailService.GetAllAsync(id);
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            ManagerDetailGetDTO? data = await _managerDetailService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Rəhbər məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ManagerDetailCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _managerDetailService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Rəhbər məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Rəhbər məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] ManagerDetailEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _managerDetailService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Rəhbər məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Rəhbər məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _managerDetailService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Rəhbər məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Rəhbər məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
