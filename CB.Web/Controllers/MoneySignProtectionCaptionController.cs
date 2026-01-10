using CB.Application.DTOs.MoneySignProtectionCaption;
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
    public class MoneySignProtectionCaptionController : ControllerBase
    {
        private readonly IMoneySignProtectionCaptionService _moneySignProtectionCaptionService;
        private readonly IValidator<MoneySignProtectionCaptionPostDTO> _validator;

        public MoneySignProtectionCaptionController(
            IMoneySignProtectionCaptionService moneySignProtectionCaptionService,
             IValidator<MoneySignProtectionCaptionPostDTO> validator
              )
        {
            _moneySignProtectionCaptionService = moneySignProtectionCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _moneySignProtectionCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] MoneySignProtectionCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _moneySignProtectionCaptionService.CreateOrUpdate(dto);

            Log.Information("Pul nişanı tarixçə mühafizə məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
