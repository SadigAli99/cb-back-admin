using CB.Application.DTOs.CoinMoneySignHistoryFeature;
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
    public class CoinMoneySignHistoryFeatureController : ControllerBase
    {
        private readonly ICoinMoneySignHistoryFeatureService _moneySignHistoryFeatureService;
        private readonly IValidator<CoinMoneySignHistoryFeaturePostDTO> _validator;

        public CoinMoneySignHistoryFeatureController(
            ICoinMoneySignHistoryFeatureService moneySignHistoryFeatureService,
             IValidator<CoinMoneySignHistoryFeaturePostDTO> validator
              )
        {
            _moneySignHistoryFeatureService = moneySignHistoryFeatureService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _moneySignHistoryFeatureService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] CoinMoneySignHistoryFeaturePostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _moneySignHistoryFeatureService.CreateOrUpdate(dto);

            Log.Information("Pul nişanı tarixçə xüsusiyyəti məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
