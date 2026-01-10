using CB.Application.DTOs.StockExchangeCaption;
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
    public class StockExchangeCaptionController : ControllerBase
    {
        private readonly IStockExchangeCaptionService _stockExchangeCaptionService;
        private readonly IValidator<StockExchangeCaptionPostDTO> _validator;

        public StockExchangeCaptionController(
            IStockExchangeCaptionService stockExchangeCaptionService,
             IValidator<StockExchangeCaptionPostDTO> validator
              )
        {
            _stockExchangeCaptionService = stockExchangeCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _stockExchangeCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] StockExchangeCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _stockExchangeCaptionService.CreateOrUpdate(dto);

            Log.Information("Stock Exchange başlıq məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
