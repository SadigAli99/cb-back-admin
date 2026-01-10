using CB.Application.DTOs.InternationalCooperationCaption;
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
    public class InternationalCooperationCaptionController : ControllerBase
    {
        private readonly IInternationalCooperationCaptionService _internationalCooperationCaptionService;
        private readonly IValidator<InternationalCooperationCaptionPostDTO> _validator;

        public InternationalCooperationCaptionController(
            IInternationalCooperationCaptionService internationalCooperationCaptionService,
             IValidator<InternationalCooperationCaptionPostDTO> validator
              )
        {
            _internationalCooperationCaptionService = internationalCooperationCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _internationalCooperationCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] InternationalCooperationCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _internationalCooperationCaptionService.CreateOrUpdate(dto);

            Log.Information("Beynəlxalq əməkdaşlıq məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
