using CB.Application.DTOs.FinancingActivityCaption;
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
    public class FinancingActivityCaptionController : ControllerBase
    {
        private readonly IFinancingActivityCaptionService _financingActivityCaptionService;
        private readonly IValidator<FinancingActivityCaptionPostDTO> _validator;

        public FinancingActivityCaptionController(
            IFinancingActivityCaptionService financingActivityCaptionService,
             IValidator<FinancingActivityCaptionPostDTO> validator
              )
        {
            _financingActivityCaptionService = financingActivityCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _financingActivityCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] FinancingActivityCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _financingActivityCaptionService.CreateOrUpdate(dto);

            Log.Information("Kreditləşmə və maliyyələşmə fəaliyyətinə dair sorğu məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
