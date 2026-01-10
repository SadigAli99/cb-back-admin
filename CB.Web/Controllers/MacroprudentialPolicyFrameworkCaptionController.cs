using CB.Application.DTOs.MacroprudentialPolicyFrameworkCaption;
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
    public class MacroprudentialPolicyFrameworkCaptionController : ControllerBase
    {
        private readonly IMacroprudentialPolicyFrameworkCaptionService _macroprudentialPolicyFrameworkCaptionService;
        private readonly IValidator<MacroprudentialPolicyFrameworkCaptionPostDTO> _validator;

        public MacroprudentialPolicyFrameworkCaptionController(
            IMacroprudentialPolicyFrameworkCaptionService macroprudentialPolicyFrameworkCaptionService,
             IValidator<MacroprudentialPolicyFrameworkCaptionPostDTO> validator
              )
        {
            _macroprudentialPolicyFrameworkCaptionService = macroprudentialPolicyFrameworkCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _macroprudentialPolicyFrameworkCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] MacroprudentialPolicyFrameworkCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _macroprudentialPolicyFrameworkCaptionService.CreateOrUpdate(dto);

            Log.Information("Makroprudensial siyasət çərçivəsi məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
