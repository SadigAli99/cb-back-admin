using CB.Application.DTOs.PolicyConcept;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.PolicyConcept;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PolicyConceptController : ControllerBase
    {
        private readonly IPolicyConceptService _policyConceptService;
        private readonly PolicyConceptCreateValidator _createValidator;
        private readonly PolicyConceptEditValidator _editValidator;

        public PolicyConceptController(
            IPolicyConceptService policyConceptService,
            PolicyConceptCreateValidator createValidator,
            PolicyConceptEditValidator editValidator
        )
        {
            _policyConceptService = policyConceptService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<PolicyConceptGetDTO> data = await _policyConceptService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            PolicyConceptGetDTO? data = await _policyConceptService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Azərbaycan Respublikası Mərkəzi Bankının risk əsaslı nəzarət üzrə Siyasət Konsepsiyası məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] PolicyConceptCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _policyConceptService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Azərbaycan Respublikası Mərkəzi Bankının risk əsaslı nəzarət üzrə Siyasət Konsepsiyası məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Azərbaycan Respublikası Mərkəzi Bankının risk əsaslı nəzarət üzrə Siyasət Konsepsiyası məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] PolicyConceptEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _policyConceptService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Azərbaycan Respublikası Mərkəzi Bankının risk əsaslı nəzarət üzrə Siyasət Konsepsiyası məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Azərbaycan Respublikası Mərkəzi Bankının risk əsaslı nəzarət üzrə Siyasət Konsepsiyası məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _policyConceptService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Azərbaycan Respublikası Mərkəzi Bankının risk əsaslı nəzarət üzrə Siyasət Konsepsiyası məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Azərbaycan Respublikası Mərkəzi Bankının risk əsaslı nəzarət üzrə Siyasət Konsepsiyası məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
