using CB.Application.DTOs.ForeignInsuranceBrokerCaption;
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
    public class ForeignInsuranceBrokerCaptionController : ControllerBase
    {
        private readonly IForeignInsuranceBrokerCaptionService _foreignInsuranceBrokerCaptionService;
        private readonly IValidator<ForeignInsuranceBrokerCaptionPostDTO> _validator;

        public ForeignInsuranceBrokerCaptionController(
            IForeignInsuranceBrokerCaptionService foreignInsuranceBrokerCaptionService,
             IValidator<ForeignInsuranceBrokerCaptionPostDTO> validator
              )
        {
            _foreignInsuranceBrokerCaptionService = foreignInsuranceBrokerCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _foreignInsuranceBrokerCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] ForeignInsuranceBrokerCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _foreignInsuranceBrokerCaptionService.CreateOrUpdate(dto);

            Log.Information("Sığorta vasitəçiləri haqqında məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
