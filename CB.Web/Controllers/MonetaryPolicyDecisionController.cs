using CB.Application.DTOs.MonetaryPolicyDecision;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.MonetaryPolicyDecision;
using CB.Shared.Helpers;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class MonetaryPolicyDecisionController : ControllerBase
    {
        private readonly IMonetaryPolicyDecisionService _monetaryPolicyDecisionService;
        private readonly MonetaryPolicyDecisionCreateValidator _createValidator;
        private readonly MonetaryPolicyDecisionEditValidator _editValidator;

        public MonetaryPolicyDecisionController(
            IMonetaryPolicyDecisionService monetaryPolicyDecisionService,
            MonetaryPolicyDecisionCreateValidator createValidator,
            MonetaryPolicyDecisionEditValidator editValidator
        )
        {
            _monetaryPolicyDecisionService = monetaryPolicyDecisionService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/monetaryPolicyDecision
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _monetaryPolicyDecisionService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/monetaryPolicyDecision/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _monetaryPolicyDecisionService.GetByIdAsync(id);
            if (data == null)
            {
                Log.Warning("Pul siyasəti qərar məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }

            return Ok(new { status = "success", data });
        }

        // POST: /api/monetaryPolicyDecision
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MonetaryPolicyDecisionCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            dto.Slugs = SlugHelper.GenerateSlugs(dto.Titles);
            var created = await _monetaryPolicyDecisionService.CreateAsync(dto);

            if (!created)
            {
                Log.Warning("Pul siyasəti qərar məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dto);
                return BadRequest();
            }
            Log.Information("Pul siyasəti qərar məlumatı uğurla əlavə olundu : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/monetaryPolicyDecision/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] MonetaryPolicyDecisionEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            dto.Slugs = SlugHelper.GenerateSlugs(dto.Titles);
            var updated = await _monetaryPolicyDecisionService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Warning("Pul siyasəti qərar məlumatı yenilənməsi uğursuz oldu : {@Dto}", dto);
                return NotFound();
            }
            Log.Information("Pul siyasəti qərar məlumatı yenilənməsi uğurludur : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        // DELETE: /api/monetaryPolicyDecision/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _monetaryPolicyDecisionService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Pul siyasəti qərar məlumatının silinməsi uğursuzdur : Id = {@Id}", id);

                return NotFound();
            }

            Log.Information("Pul siyasəti qərar məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }
    }
}
