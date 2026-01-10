using CB.Application.DTOs.PaymentAgentCaption;
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
    public class PaymentAgentCaptionController : ControllerBase
    {
        private readonly IPaymentAgentCaptionService _paymentAgentCaptionService;
        private readonly IValidator<PaymentAgentCaptionPostDTO> _validator;

        public PaymentAgentCaptionController(
            IPaymentAgentCaptionService paymentAgentCaptionService,
             IValidator<PaymentAgentCaptionPostDTO> validator
              )
        {
            _paymentAgentCaptionService = paymentAgentCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _paymentAgentCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] PaymentAgentCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _paymentAgentCaptionService.CreateOrUpdate(dto);

            Log.Information("Bank başlıq məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
