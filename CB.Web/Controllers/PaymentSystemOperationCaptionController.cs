using CB.Application.DTOs.PaymentSystemOperationCaption;
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
    public class PaymentSystemOperationCaptionController : ControllerBase
    {
        private readonly IPaymentSystemOperationCaptionService _paymentSystemOperationCaptionService;
        private readonly IValidator<PaymentSystemOperationCaptionPostDTO> _validator;

        public PaymentSystemOperationCaptionController(
            IPaymentSystemOperationCaptionService paymentSystemOperationCaptionService,
             IValidator<PaymentSystemOperationCaptionPostDTO> validator
              )
        {
            _paymentSystemOperationCaptionService = paymentSystemOperationCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _paymentSystemOperationCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] PaymentSystemOperationCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _paymentSystemOperationCaptionService.CreateOrUpdate(dto);

            Log.Information("Ödəmə sistemləri operatorları başlıq məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
