using CB.Application.DTOs.PaymentInstitutionCaption;
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
    public class PaymentInstitutionCaptionController : ControllerBase
    {
        private readonly IPaymentInstitutionCaptionService _paymentInstitutionCaptionService;
        private readonly IValidator<PaymentInstitutionCaptionPostDTO> _validator;

        public PaymentInstitutionCaptionController(
            IPaymentInstitutionCaptionService PpymentInstitutionCaptionService,
             IValidator<PaymentInstitutionCaptionPostDTO> validator
              )
        {
            _paymentInstitutionCaptionService = PpymentInstitutionCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _paymentInstitutionCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] PaymentInstitutionCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _paymentInstitutionCaptionService.CreateOrUpdate(dto);

            Log.Information("Ödəniş təşkilatları başlıq məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
