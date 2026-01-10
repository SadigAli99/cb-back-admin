using CB.Application.DTOs.PostalCommunicationCaption;
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
    public class PostalCommunicationCaptionController : ControllerBase
    {
        private readonly IPostalCommunicationCaptionService _postalCommunicationCaptionService;
        private readonly IValidator<PostalCommunicationCaptionPostDTO> _validator;

        public PostalCommunicationCaptionController(
            IPostalCommunicationCaptionService postalCommunicationCaptionService,
             IValidator<PostalCommunicationCaptionPostDTO> validator
              )
        {
            _postalCommunicationCaptionService = postalCommunicationCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _postalCommunicationCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] PostalCommunicationCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _postalCommunicationCaptionService.CreateOrUpdate(dto);

            Log.Information("Poçt rabitəsinin milli operatoru haqqında məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
