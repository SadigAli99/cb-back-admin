using CB.Application.DTOs.QualificationCertificateCaption;
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
    public class QualificationCertificateCaptionController : ControllerBase
    {
        private readonly IQualificationCertificateCaptionService _qualificationCertificateCaptionService;
        private readonly IValidator<QualificationCertificateCaptionPostDTO> _validator;

        public QualificationCertificateCaptionController(
            IQualificationCertificateCaptionService qualificationCertificateCaptionService,
             IValidator<QualificationCertificateCaptionPostDTO> validator
              )
        {
            _qualificationCertificateCaptionService = qualificationCertificateCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _qualificationCertificateCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] QualificationCertificateCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _qualificationCertificateCaptionService.CreateOrUpdate(dto);

            Log.Information("İxtisas şəhadətnamələri haqqında məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
