using CB.Application.DTOs.MacroprudentialPolicyFramework;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.MacroprudentialPolicyFramework;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MacroprudentialPolicyFrameworkController : ControllerBase
    {
        private readonly IMacroprudentialPolicyFrameworkService _macroprudentialPolicyFrameworkService;
        private readonly MacroprudentialPolicyFrameworkCreateValidator _createValidator;
        private readonly MacroprudentialPolicyFrameworkEditValidator _editValidator;

        public MacroprudentialPolicyFrameworkController(
            IMacroprudentialPolicyFrameworkService macroprudentialPolicyFrameworkService,
            MacroprudentialPolicyFrameworkCreateValidator createValidator,
            MacroprudentialPolicyFrameworkEditValidator editValidator
        )
        {
            _macroprudentialPolicyFrameworkService = macroprudentialPolicyFrameworkService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<MacroprudentialPolicyFrameworkGetDTO> data = await _macroprudentialPolicyFrameworkService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            MacroprudentialPolicyFrameworkGetDTO? data = await _macroprudentialPolicyFrameworkService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Makroprudensial siyasət çərçivəsi məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] MacroprudentialPolicyFrameworkCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _macroprudentialPolicyFrameworkService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Makroprudensial siyasət çərçivəsi məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Makroprudensial siyasət çərçivəsi məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] MacroprudentialPolicyFrameworkEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _macroprudentialPolicyFrameworkService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Makroprudensial siyasət çərçivəsi məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Makroprudensial siyasət çərçivəsi məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _macroprudentialPolicyFrameworkService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Makroprudensial siyasət çərçivəsi məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Makroprudensial siyasət çərçivəsi məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
