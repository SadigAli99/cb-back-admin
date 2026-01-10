using CB.Application.DTOs.LicensingProcessCaption;
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
    public class LicensingProcessCaptionController : ControllerBase
    {
        private readonly ILicensingProcessCaptionService _licensingProcessCaptionService;
        private readonly IValidator<LicensingProcessCaptionPostDTO> _validator;

        public LicensingProcessCaptionController(
            ILicensingProcessCaptionService licensingProcessCaptionService,
             IValidator<LicensingProcessCaptionPostDTO> validator
              )
        {
            _licensingProcessCaptionService = licensingProcessCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _licensingProcessCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] LicensingProcessCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _licensingProcessCaptionService.CreateOrUpdate(dto);

            Log.Information("Lizensiya prosesləri haqqında məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
