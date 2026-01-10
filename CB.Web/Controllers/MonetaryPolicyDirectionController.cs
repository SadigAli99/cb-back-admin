using CB.Application.DTOs.MonetaryPolicyDirection;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.MonetaryPolicyDirection;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MonetaryPolicyDirectionController : ControllerBase
    {
        private readonly IMonetaryPolicyDirectionService _monetaryPolicyDirectionService;
        private readonly MonetaryPolicyDirectionCreateValidator _createValidator;
        private readonly MonetaryPolicyDirectionEditValidator _editValidator;

        public MonetaryPolicyDirectionController(
            IMonetaryPolicyDirectionService monetaryPolicyDirectionService,
            MonetaryPolicyDirectionCreateValidator createValidator,
            MonetaryPolicyDirectionEditValidator editValidator
        )
        {
            _monetaryPolicyDirectionService = monetaryPolicyDirectionService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<MonetaryPolicyDirectionGetDTO> data = await _monetaryPolicyDirectionService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            MonetaryPolicyDirectionGetDTO? data = await _monetaryPolicyDirectionService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Statistik bülleten məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] MonetaryPolicyDirectionCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _monetaryPolicyDirectionService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Statistik bülleten məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Statistik bülleten məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] MonetaryPolicyDirectionEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _monetaryPolicyDirectionService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Statistik bülleten məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Statistik bülleten məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _monetaryPolicyDirectionService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Statistik bülleten məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Statistik bülleten məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
