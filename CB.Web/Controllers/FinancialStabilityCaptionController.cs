using CB.Application.DTOs.FinancialStabilityCaption;
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
    public class FinancialStabilityCaptionController : ControllerBase
    {
        private readonly IFinancialStabilityCaptionService _financialStabilityCaptionService;
        private readonly IValidator<FinancialStabilityCaptionPostDTO> _validator;

        public FinancialStabilityCaptionController(
            IFinancialStabilityCaptionService financialStabilityCaptionService,
             IValidator<FinancialStabilityCaptionPostDTO> validator
              )
        {
            _financialStabilityCaptionService = financialStabilityCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _financialStabilityCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] FinancialStabilityCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _financialStabilityCaptionService.CreateOrUpdate(dto);

            Log.Information("Maliyyə sabitliyi məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
