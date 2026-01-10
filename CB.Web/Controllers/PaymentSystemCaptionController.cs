using CB.Application.DTOs.PaymentSystemCaption;
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
    public class PaymentSystemCaptionController : ControllerBase
    {
        private readonly IPaymentSystemCaptionService _paymentSystemCaptionService;
        private readonly IValidator<PaymentSystemCaptionPostDTO> _validator;

        public PaymentSystemCaptionController(
            IPaymentSystemCaptionService paymentSystemCaptionService,
             IValidator<PaymentSystemCaptionPostDTO> validator
              )
        {
            _paymentSystemCaptionService = paymentSystemCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _paymentSystemCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] PaymentSystemCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _paymentSystemCaptionService.CreateOrUpdate(dto);

            Log.Information("Ödəniş sistemləri məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
