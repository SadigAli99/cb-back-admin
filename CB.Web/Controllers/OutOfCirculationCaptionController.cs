using CB.Application.DTOs.OutOfCirculationCaption;
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
    public class OutOfCirculationCaptionController : ControllerBase
    {
        private readonly IOutOfCirculationCaptionService _outOfCirculationCaptionService;
        private readonly IValidator<OutOfCirculationCaptionPostDTO> _validator;

        public OutOfCirculationCaptionController(
            IOutOfCirculationCaptionService outOfCirculationCaptionService,
             IValidator<OutOfCirculationCaptionPostDTO> validator
              )
        {
            _outOfCirculationCaptionService = outOfCirculationCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _outOfCirculationCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] OutOfCirculationCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _outOfCirculationCaptionService.CreateOrUpdate(dto);

            Log.Information("Tədavüldən kənar pul nişanları məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
