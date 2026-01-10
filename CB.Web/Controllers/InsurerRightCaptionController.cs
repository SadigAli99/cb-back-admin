using CB.Application.DTOs.InsurerRightCaption;
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
    public class InsurerRightCaptionController : ControllerBase
    {
        private readonly IInsurerRightCaptionService _insurerRightCaptionService;
        private readonly IValidator<InsurerRightCaptionPostDTO> _validator;

        public InsurerRightCaptionController(
            IInsurerRightCaptionService insurerRightCaptionService,
             IValidator<InsurerRightCaptionPostDTO> validator
              )
        {
            _insurerRightCaptionService = insurerRightCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _insurerRightCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] InsurerRightCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _insurerRightCaptionService.CreateOrUpdate(dto);

            Log.Information("Haqqımızda məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
