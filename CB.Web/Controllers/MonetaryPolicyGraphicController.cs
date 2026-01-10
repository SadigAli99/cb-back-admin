using CB.Application.DTOs.MonetaryPolicyGraphic;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.MonetaryPolicyGraphic;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MonetaryPolicyGraphicController : ControllerBase
    {
        private readonly IMonetaryPolicyGraphicService _monetaryPolicyGraphicService;
        private readonly MonetaryPolicyGraphicCreateValidator _createValidator;
        private readonly MonetaryPolicyGraphicEditValidator _editValidator;

        public MonetaryPolicyGraphicController(
            IMonetaryPolicyGraphicService monetaryPolicyGraphicService,
            MonetaryPolicyGraphicCreateValidator createValidator,
            MonetaryPolicyGraphicEditValidator editValidator
        )
        {
            _monetaryPolicyGraphicService = monetaryPolicyGraphicService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<MonetaryPolicyGraphicGetDTO> data = await _monetaryPolicyGraphicService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            MonetaryPolicyGraphicGetDTO? data = await _monetaryPolicyGraphicService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Pul siyasəti qrafik məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MonetaryPolicyGraphicCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _monetaryPolicyGraphicService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Pul siyasəti qrafik məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Pul siyasəti qrafik məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] MonetaryPolicyGraphicEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _monetaryPolicyGraphicService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Pul siyasəti qrafik məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Pul siyasəti qrafik məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _monetaryPolicyGraphicService.DeleteAsync(id);

            if (!deleted)
            {
                Log.Warning("Pul siyasəti qrafik məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Pul siyasəti qrafik məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });


        }

    }
}
