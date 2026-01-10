using CB.Application.DTOs.NominationCaption;
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
    public class NominationCaptionController : ControllerBase
    {
        private readonly INominationCaptionService _nominationCaptionService;
        private readonly IValidator<NominationCaptionPostDTO> _validator;

        public NominationCaptionController(
            INominationCaptionService nominationCaptionService,
             IValidator<NominationCaptionPostDTO> validator
              )
        {
            _nominationCaptionService = nominationCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _nominationCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] NominationCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _nominationCaptionService.CreateOrUpdate(dto);

            Log.Information("Nominasiya məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
