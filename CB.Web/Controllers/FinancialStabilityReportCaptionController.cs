using CB.Application.DTOs.FinancialStabilityReportCaption;
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
    public class FinancialStabilityReportCaptionController : ControllerBase
    {
        private readonly IFinancialStabilityReportCaptionService _financialStabilityReportCaptionService;
        private readonly IValidator<FinancialStabilityReportCaptionPostDTO> _validator;

        public FinancialStabilityReportCaptionController(
            IFinancialStabilityReportCaptionService financialStabilityReportCaptionService,
             IValidator<FinancialStabilityReportCaptionPostDTO> validator
              )
        {
            _financialStabilityReportCaptionService = financialStabilityReportCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _financialStabilityReportCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] FinancialStabilityReportCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _financialStabilityReportCaptionService.CreateOrUpdate(dto);

            Log.Information("Maliyyə sabitliyi hesabat məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
