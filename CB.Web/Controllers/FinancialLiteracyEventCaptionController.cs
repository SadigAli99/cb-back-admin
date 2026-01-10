using CB.Application.DTOs.FinancialLiteracyEventCaption;
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
    public class FinancialLiteracyEventCaptionController : ControllerBase
    {
        private readonly IFinancialLiteracyEventCaptionService _financialLiteracyEventCaptionService;
        private readonly IValidator<FinancialLiteracyEventCaptionPostDTO> _validator;

        public FinancialLiteracyEventCaptionController(
            IFinancialLiteracyEventCaptionService financialLiteracyEventCaptionService,
             IValidator<FinancialLiteracyEventCaptionPostDTO> validator
              )
        {
            _financialLiteracyEventCaptionService = financialLiteracyEventCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _financialLiteracyEventCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromForm] FinancialLiteracyEventCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _financialLiteracyEventCaptionService.CreateOrUpdate(dto);

            Log.Information("Maliyyə savadlılığı tədbir məlumatları uğurla yeniləndi : {@Dto}", dto);

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });

        }
    }
}
