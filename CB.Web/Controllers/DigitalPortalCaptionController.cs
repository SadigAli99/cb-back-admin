using CB.Application.DTOs.DigitalPortalCaption;
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
    public class DigitalPortalCaptionController : ControllerBase
    {
        private readonly IDigitalPortalCaptionService _digialPortalCaptionService;
        private readonly IValidator<DigitalPortalCaptionPostDTO> _validator;

        public DigitalPortalCaptionController(
            IDigitalPortalCaptionService digialPortalCaptionService,
             IValidator<DigitalPortalCaptionPostDTO> validator
              )
        {
            _digialPortalCaptionService = digialPortalCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _digialPortalCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] DigitalPortalCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _digialPortalCaptionService.CreateOrUpdate(dto);

            Log.Information("Rəqəmsal portal başlıq bölməsi yenilənməsi uğurludur : {@Dto}", dto);

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
