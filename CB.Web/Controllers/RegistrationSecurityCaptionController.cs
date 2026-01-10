using CB.Application.DTOs.RegistrationSecurityCaption;
using CB.Application.Interfaces.Services;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegistrationSecurityCaptionController : ControllerBase
    {
        private readonly IRegistrationSecurityCaptionService _registrationSecurityCaptionService;
        private readonly IValidator<RegistrationSecurityCaptionPostDTO> _validator;

        public RegistrationSecurityCaptionController(
            IRegistrationSecurityCaptionService registrationSecurityCaptionService,
             IValidator<RegistrationSecurityCaptionPostDTO> validator
              )
        {
            _registrationSecurityCaptionService = registrationSecurityCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _registrationSecurityCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] RegistrationSecurityCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _registrationSecurityCaptionService.CreateOrUpdate(dto);

            Log.Information("Haqqımızda məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
