using CB.Application.DTOs.BankInvestmentCaption;
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
    public class BankInvestmentCaptionController : ControllerBase
    {
        private readonly IBankInvestmentCaptionService _bankInvestmentCaptionService;
        private readonly IValidator<BankInvestmentCaptionPostDTO> _validator;

        public BankInvestmentCaptionController(
            IBankInvestmentCaptionService bankInvestmentCaptionService,
             IValidator<BankInvestmentCaptionPostDTO> validator
              )
        {
            _bankInvestmentCaptionService = bankInvestmentCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _bankInvestmentCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] BankInvestmentCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _bankInvestmentCaptionService.CreateOrUpdate(dto);

            Log.Information("Bank xidmətlərinin investisiyası başlıq məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
