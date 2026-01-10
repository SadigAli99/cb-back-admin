using CB.Application.DTOs.RegistrationSecurity;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.RegistrationSecurity;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RegistrationSecurityController : ControllerBase
    {
        private readonly IRegistrationSecurityService _registrationSecurityService;
        private readonly RegistrationSecurityCreateValidator _createValidator;
        private readonly RegistrationSecurityEditValidator _editValidator;

        public RegistrationSecurityController(
            IRegistrationSecurityService registrationSecurityService,
            RegistrationSecurityCreateValidator createValidator,
            RegistrationSecurityEditValidator editValidator
        )
        {
            _registrationSecurityService = registrationSecurityService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RegistrationSecurityGetDTO> data = await _registrationSecurityService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            RegistrationSecurityGetDTO? data = await _registrationSecurityService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Dövlət qeydiyyatı ilə bağlı təlimat məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RegistrationSecurityCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _registrationSecurityService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Dövlət qeydiyyatı ilə bağlı təlimat məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Dövlət qeydiyyatı ilə bağlı təlimat məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] RegistrationSecurityEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _registrationSecurityService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Dövlət qeydiyyatı ilə bağlı təlimat məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Dövlət qeydiyyatı ilə bağlı təlimat məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _registrationSecurityService.DeleteAsync(id);

            if (!deleted)
            {
                Log.Warning("Dövlət qeydiyyatı ilə bağlı təlimat məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Dövlət qeydiyyatı ilə bağlı təlimat məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });


        }

    }
}
