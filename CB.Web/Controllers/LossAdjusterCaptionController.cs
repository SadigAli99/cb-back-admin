using CB.Application.DTOs.LossAdjusterCaption;
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
    public class LossAdjusterCaptionController : ControllerBase
    {
        private readonly ILossAdjusterCaptionService _lossAdjusterCaptionService;
        private readonly IValidator<LossAdjusterCaptionPostDTO> _validator;

        public LossAdjusterCaptionController(
            ILossAdjusterCaptionService lossAdjusterCaptionService,
             IValidator<LossAdjusterCaptionPostDTO> validator
              )
        {
            _lossAdjusterCaptionService = lossAdjusterCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _lossAdjusterCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] LossAdjusterCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _lossAdjusterCaptionService.CreateOrUpdate(dto);

            Log.Information("Zərər tənzimləyicisi haqqında məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
