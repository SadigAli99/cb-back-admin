using CB.Application.DTOs.PublicationCaption;
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
    public class PublicationCaptionController : ControllerBase
    {
        private readonly IPublicationCaptionService _publicationCaptionService;
        private readonly IValidator<PublicationCaptionPostDTO> _validator;

        public PublicationCaptionController(
            IPublicationCaptionService publicationCaptionService,
             IValidator<PublicationCaptionPostDTO> validator
              )
        {
            _publicationCaptionService = publicationCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _publicationCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] PublicationCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _publicationCaptionService.CreateOrUpdate(dto);

            Log.Information("Nəşrlər və tədqiqatlar məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
