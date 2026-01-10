using CB.Application.DTOs.OutOfCoinMoneySignHistoryFeature;
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
    public class OutOfCoinMoneySignHistoryFeatureController : ControllerBase
    {
        private readonly IOutOfCoinMoneySignHistoryFeatureService _moneySignHistoryFeatureService;
        private readonly IValidator<OutOfCoinMoneySignHistoryFeaturePostDTO> _validator;

        public OutOfCoinMoneySignHistoryFeatureController(
            IOutOfCoinMoneySignHistoryFeatureService moneySignHistoryFeatureService,
             IValidator<OutOfCoinMoneySignHistoryFeaturePostDTO> validator
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
        public async Task<IActionResult> Submit([FromBody] OutOfCoinMoneySignHistoryFeaturePostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _moneySignHistoryFeatureService.CreateOrUpdate(dto);

            Log.Information("Tədavüldən kənar metal pul nişanı tarixçə xüsusiyyəti məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
