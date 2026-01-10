using CB.Application.DTOs.CurrencyRegulationRightCaption;
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
    public class CurrencyRegulationRightCaptionController : ControllerBase
    {
        private readonly ICurrencyRegulationRightCaptionService _currencyRegulationRightCaptionService;
        private readonly IValidator<CurrencyRegulationRightCaptionPostDTO> _validator;

        public CurrencyRegulationRightCaptionController(
            ICurrencyRegulationRightCaptionService currencyRegulationRightCaptionService,
             IValidator<CurrencyRegulationRightCaptionPostDTO> validator
              )
        {
            _currencyRegulationRightCaptionService = currencyRegulationRightCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _currencyRegulationRightCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] CurrencyRegulationRightCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _currencyRegulationRightCaptionService.CreateOrUpdate(dto);

            Log.Information("Maliyyə tənzimləmə qaydalar məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
