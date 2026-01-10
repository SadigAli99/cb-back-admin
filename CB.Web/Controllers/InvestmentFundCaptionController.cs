using CB.Application.DTOs.InvestmentFundCaption;
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
    public class InvestmentFundCaptionController : ControllerBase
    {
        private readonly IInvestmentFundCaptionService _investmentFundCaptionService;
        private readonly IValidator<InvestmentFundCaptionPostDTO> _validator;

        public InvestmentFundCaptionController(
            IInvestmentFundCaptionService investmentFundCaptionService,
             IValidator<InvestmentFundCaptionPostDTO> validator
              )
        {
            _investmentFundCaptionService = investmentFundCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _investmentFundCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] InvestmentFundCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _investmentFundCaptionService.CreateOrUpdate(dto);

            Log.Information("İnvestisiya fondları başlıq məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
