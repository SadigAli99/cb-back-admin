using CB.Application.DTOs.InstantPaymentOrganization;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.InstantPaymentOrganization;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InstantPaymentOrganizationController : ControllerBase
    {
        private readonly IInstantPaymentOrganizationService _instantPaymentOrganizationService;
        private readonly InstantPaymentOrganizationCreateValidator _createValidator;
        private readonly InstantPaymentOrganizationEditValidator _editValidator;

        public InstantPaymentOrganizationController(
            IInstantPaymentOrganizationService instantPaymentOrganizationService,
            InstantPaymentOrganizationCreateValidator createValidator,
            InstantPaymentOrganizationEditValidator editValidator
        )
        {
            _instantPaymentOrganizationService = instantPaymentOrganizationService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<InstantPaymentOrganizationGetDTO> data = await _instantPaymentOrganizationService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            InstantPaymentOrganizationGetDTO? data = await _instantPaymentOrganizationService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Ani ödəmə təşkilatı məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InstantPaymentOrganizationCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _instantPaymentOrganizationService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Ani ödəmə təşkilatı məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Ani ödəmə təşkilatı məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] InstantPaymentOrganizationEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _instantPaymentOrganizationService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Ani ödəmə təşkilatı məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Ani ödəmə təşkilatı məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _instantPaymentOrganizationService.DeleteAsync(id);

            if (!deleted)
            {
                Log.Warning("Ani ödəmə təşkilatı məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Ani ödəmə təşkilatı məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });


        }

    }
}
