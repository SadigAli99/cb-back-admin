using CB.Application.DTOs.InsuranceBrokerCaption;
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
    public class InsuranceBrokerCaptionController : ControllerBase
    {
        private readonly IInsuranceBrokerCaptionService _insuranceBrokerCaptionService;
        private readonly IValidator<InsuranceBrokerCaptionPostDTO> _validator;

        public InsuranceBrokerCaptionController(
            IInsuranceBrokerCaptionService insuranceBrokerCaptionService,
             IValidator<InsuranceBrokerCaptionPostDTO> validator
              )
        {
            _insuranceBrokerCaptionService = insuranceBrokerCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _insuranceBrokerCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] InsuranceBrokerCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _insuranceBrokerCaptionService.CreateOrUpdate(dto);

            Log.Information("Sığorta vasitəçiləri haqqında məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
