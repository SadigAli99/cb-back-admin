using CB.Application.DTOs.FraudStatisticCaption;
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
    public class FraudStatisticCaptionController : ControllerBase
    {
        private readonly IFraudStatisticCaptionService _fraudStatisticCaptionService;
        private readonly IValidator<FraudStatisticCaptionPostDTO> _validator;

        public FraudStatisticCaptionController(
            IFraudStatisticCaptionService fraudStatisticCaptionService,
             IValidator<FraudStatisticCaptionPostDTO> validator
              )
        {
            _fraudStatisticCaptionService = fraudStatisticCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _fraudStatisticCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] FraudStatisticCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _fraudStatisticCaptionService.CreateOrUpdate(dto);

            Log.Information("Fırıldaqçılıq statistikası məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
