using CB.Application.DTOs.GovernmentPaymentPortal;
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
    public class GovernmentPaymentPortalController : ControllerBase
    {
        private readonly IGovernmentPaymentPortalService _governmentPaymenyPortalService;
        private readonly IValidator<GovernmentPaymentPortalPostDTO> _validator;

        public GovernmentPaymentPortalController(
            IGovernmentPaymentPortalService governmentPaymenyPortalService,
             IValidator<GovernmentPaymentPortalPostDTO> validator
              )
        {
            _governmentPaymenyPortalService = governmentPaymenyPortalService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _governmentPaymenyPortalService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] GovernmentPaymentPortalPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _governmentPaymenyPortalService.CreateOrUpdate(dto);

            Log.Information("Hökumət ödənişi portalı məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
