using CB.Application.DTOs.PolicyAnalysis;
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
    public class PolicyAnalysisController : ControllerBase
    {
        private readonly IPolicyAnalysisService _policyAnalysisService;
        private readonly IValidator<PolicyAnalysisPostDTO> _validator;

        public PolicyAnalysisController(
            IPolicyAnalysisService policyAnalysisService,
             IValidator<PolicyAnalysisPostDTO> validator
              )
        {
            _policyAnalysisService = policyAnalysisService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _policyAnalysisService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] PolicyAnalysisPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _policyAnalysisService.CreateOrUpdate(dto);

            Log.Information("Haqqımızda məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
