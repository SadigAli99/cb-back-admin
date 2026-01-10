using CB.Application.DTOs.MonetaryPolicyInstrument;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.MonetaryPolicyInstrument;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MonetaryPolicyInstrumentController : ControllerBase
    {
        private readonly IMonetaryPolicyInstrumentService _monetaryPolicyInstrumentService;
        private readonly MonetaryPolicyInstrumentCreateValidator _createValidator;
        private readonly MonetaryPolicyInstrumentEditValidator _editValidator;

        public MonetaryPolicyInstrumentController(
            IMonetaryPolicyInstrumentService monetaryPolicyInstrumentService,
            MonetaryPolicyInstrumentCreateValidator createValidator,
            MonetaryPolicyInstrumentEditValidator editValidator
        )
        {
            _monetaryPolicyInstrumentService = monetaryPolicyInstrumentService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<MonetaryPolicyInstrumentGetDTO> data = await _monetaryPolicyInstrumentService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            MonetaryPolicyInstrumentGetDTO? data = await _monetaryPolicyInstrumentService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Pul siyasəti aləti məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] MonetaryPolicyInstrumentCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _monetaryPolicyInstrumentService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Pul siyasəti aləti məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Pul siyasəti aləti məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] MonetaryPolicyInstrumentEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _monetaryPolicyInstrumentService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Pul siyasəti aləti məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Pul siyasəti aləti məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _monetaryPolicyInstrumentService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Pul siyasəti aləti məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Pul siyasəti aləti məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
