using CB.Application.DTOs.ExecutionStatus;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.ExecutionStatus;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ExecutionStatusController : ControllerBase
    {
        private readonly IExecutionStatusService _executionStatusService;
        private readonly ExecutionStatusCreateValidator _createValidator;
        private readonly ExecutionStatusEditValidator _editValidator;

        public ExecutionStatusController(
            IExecutionStatusService executionStatusService,
            ExecutionStatusCreateValidator createValidator,
            ExecutionStatusEditValidator editValidator
        )
        {
            _executionStatusService = executionStatusService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<ExecutionStatusGetDTO> data = await _executionStatusService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            ExecutionStatusGetDTO? data = await _executionStatusService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("İcra statusu məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ExecutionStatusCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _executionStatusService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("İcra statusu məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("İcra statusu məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] ExecutionStatusEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _executionStatusService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("İcra statusu məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("İcra statusu məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _executionStatusService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("İcra statusu məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("İcra statusu məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
