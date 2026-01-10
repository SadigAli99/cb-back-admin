using CB.Application.DTOs.MediaCaption;
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
    public class MediaCaptionController : ControllerBase
    {
        private readonly IMediaCaptionService _mediaCaptionService;
        private readonly IValidator<MediaCaptionPostDTO> _validator;

        public MediaCaptionController(
            IMediaCaptionService mediaCaptionService,
             IValidator<MediaCaptionPostDTO> validator
              )
        {
            _mediaCaptionService = mediaCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _mediaCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] MediaCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _mediaCaptionService.CreateOrUpdate(dto);

            Log.Information("Media məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
