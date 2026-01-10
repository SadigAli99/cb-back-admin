using CB.Application.DTOs.InvestmentCompanyCaption;
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
    public class InvestmentCompanyCaptionController : ControllerBase
    {
        private readonly IInvestmentCompanyCaptionService _investmentCompanyCaptionService;
        private readonly IValidator<InvestmentCompanyCaptionPostDTO> _validator;

        public InvestmentCompanyCaptionController(
            IInvestmentCompanyCaptionService investmentCompanyCaptionService,
             IValidator<InvestmentCompanyCaptionPostDTO> validator
              )
        {
            _investmentCompanyCaptionService = investmentCompanyCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _investmentCompanyCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] InvestmentCompanyCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _investmentCompanyCaptionService.CreateOrUpdate(dto);

            Log.Information("İnvestisiya şirkətləri başlıq məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
