using CB.Application.DTOs.MembershipInternationalOrganization;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.MembershipInternationalOrganization;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MembershipInternationalOrganizationController : ControllerBase
    {
        private readonly IMembershipInternationalOrganizationService _membershipInternationalOrganizationService;
        private readonly MembershipInternationalOrganizationCreateValidator _createValidator;
        private readonly MembershipInternationalOrganizationEditValidator _editValidator;

        public MembershipInternationalOrganizationController(
            IMembershipInternationalOrganizationService membershipInternationalOrganizationService,
            MembershipInternationalOrganizationCreateValidator createValidator,
            MembershipInternationalOrganizationEditValidator editValidator
        )
        {
            _membershipInternationalOrganizationService = membershipInternationalOrganizationService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<MembershipInternationalOrganizationGetDTO> data = await _membershipInternationalOrganizationService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            MembershipInternationalOrganizationGetDTO? data = await _membershipInternationalOrganizationService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Beynəlxalq təşkilatlarda üzvlük məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MembershipInternationalOrganizationCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _membershipInternationalOrganizationService.CreateAsync(dTO);

            if (!created)
            {
                Log.Warning("Beynəlxalq təşkilatlarda üzvlük məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Beynəlxalq təşkilatlarda üzvlük məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] MembershipInternationalOrganizationEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _membershipInternationalOrganizationService.UpdateAsync(id, dTO);

            if (!updated)
            {
                Log.Warning("Beynəlxalq təşkilatlarda üzvlük məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Beynəlxalq təşkilatlarda üzvlük məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _membershipInternationalOrganizationService.DeleteAsync(id);

            if (!deleted)
            {
                Log.Warning("Beynəlxalq təşkilatlarda üzvlük məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Beynəlxalq təşkilatlarda üzvlük məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
