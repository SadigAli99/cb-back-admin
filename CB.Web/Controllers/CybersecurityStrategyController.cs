using CB.Application.DTOs.CybersecurityStrategy;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.CybersecurityStrategy;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CybersecurityStrategyController : ControllerBase
    {
        private readonly ICybersecurityStrategyService _cybersecurityStrategyService;
        private readonly CybersecurityStrategyCreateValidator _createValidator;
        private readonly CybersecurityStrategyEditValidator _editValidator;

        public CybersecurityStrategyController(
            ICybersecurityStrategyService cybersecurityStrategyService,
            CybersecurityStrategyCreateValidator createValidator,
            CybersecurityStrategyEditValidator editValidator
        )
        {
            _cybersecurityStrategyService = cybersecurityStrategyService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<CybersecurityStrategyGetDTO> data = await _cybersecurityStrategyService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            CybersecurityStrategyGetDTO? data = await _cybersecurityStrategyService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Maliyyə bazarlarında kibertəhlükəsizlik Strategiyası məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CybersecurityStrategyCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _cybersecurityStrategyService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Maliyyə bazarlarında kibertəhlükəsizlik Strategiyası məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Maliyyə bazarlarında kibertəhlükəsizlik Strategiyası məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] CybersecurityStrategyEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _cybersecurityStrategyService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Maliyyə bazarlarında kibertəhlükəsizlik Strategiyası məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Maliyyə bazarlarında kibertəhlükəsizlik Strategiyası məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _cybersecurityStrategyService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Maliyyə bazarlarında kibertəhlükəsizlik Strategiyası məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Maliyyə bazarlarında kibertəhlükəsizlik Strategiyası məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
