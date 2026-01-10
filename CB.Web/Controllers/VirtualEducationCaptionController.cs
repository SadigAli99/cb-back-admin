using CB.Application.DTOs.VirtualEducationCaption;
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
    public class VirtualEducationCaptionController : ControllerBase
    {
        private readonly IVirtualEducationCaptionService _financialLiteracyPortalCaptionService;
        private readonly IValidator<VirtualEducationCaptionPostDTO> _validator;

        public VirtualEducationCaptionController(
            IVirtualEducationCaptionService financialLiteracyPortalCaptionService,
             IValidator<VirtualEducationCaptionPostDTO> validator
              )
        {
            _financialLiteracyPortalCaptionService = financialLiteracyPortalCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _financialLiteracyPortalCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromForm] VirtualEducationCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _financialLiteracyPortalCaptionService.CreateOrUpdate(dto);

            Log.Information("Virtual təhsil portal məlumatları uğurla yeniləndi : {@Dto}", dto);

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });

        }
    }
}
