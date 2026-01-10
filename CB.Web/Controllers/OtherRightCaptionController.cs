using CB.Application.DTOs.OtherRightCaption;
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
    public class OtherRightCaptionController : ControllerBase
    {
        private readonly IOtherRightCaptionService _otherRightCaptionService;
        private readonly IValidator<OtherRightCaptionPostDTO> _validator;

        public OtherRightCaptionController(
            IOtherRightCaptionService otherRightCaptionService,
             IValidator<OtherRightCaptionPostDTO> validator
              )
        {
            _otherRightCaptionService = otherRightCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _otherRightCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] OtherRightCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _otherRightCaptionService.CreateOrUpdate(dto);

            Log.Information("Digər qaydalar məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
