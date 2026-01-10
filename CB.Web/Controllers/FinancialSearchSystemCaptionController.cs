using CB.Application.DTOs.FinancialSearchSystemCaption;
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
    public class FinancialSearchSystemCaptionController : ControllerBase
    {
        private readonly IFinancialSearchSystemCaptionService _financialSearchSystemCaptionService;
        private readonly IValidator<FinancialSearchSystemCaptionPostDTO> _validator;

        public FinancialSearchSystemCaptionController(
            IFinancialSearchSystemCaptionService financialSearchSystemCaptionService,
             IValidator<FinancialSearchSystemCaptionPostDTO> validator
              )
        {
            _financialSearchSystemCaptionService = financialSearchSystemCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _financialSearchSystemCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromForm] FinancialSearchSystemCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _financialSearchSystemCaptionService.CreateOrUpdate(dto);

            Log.Information("Maliyyə məhsul və xidmətlərinin məlumat-axtarış sistemi uğurla yeniləndi : {@Dto}", dto);

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });

        }
    }
}
