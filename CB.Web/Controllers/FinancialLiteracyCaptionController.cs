using CB.Application.DTOs.FinancialLiteracyCaption;
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
    public class FinancialLiteracyCaptionController : ControllerBase
    {
        private readonly IFinancialLiteracyCaptionService _financialLiteracyCaptionService;
        private readonly IValidator<FinancialLiteracyCaptionPostDTO> _validator;

        public FinancialLiteracyCaptionController(
            IFinancialLiteracyCaptionService financialLiteracyCaptionService,
             IValidator<FinancialLiteracyCaptionPostDTO> validator
              )
        {
            _financialLiteracyCaptionService = financialLiteracyCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _financialLiteracyCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] FinancialLiteracyCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _financialLiteracyCaptionService.CreateOrUpdate(dto);

            Log.Information("Maliyyə savadlılığı məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
