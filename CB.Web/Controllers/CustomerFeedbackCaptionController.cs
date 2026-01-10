using CB.Application.DTOs.CustomerFeedbackCaption;
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
    public class CustomerFeedbackCaptionController : ControllerBase
    {
        private readonly ICustomerFeedbackCaptionService _customerFeedbackCaptionService;
        private readonly IValidator<CustomerFeedbackCaptionPostDTO> _validator;

        public CustomerFeedbackCaptionController(
            ICustomerFeedbackCaptionService customerFeedbackCaptionService,
             IValidator<CustomerFeedbackCaptionPostDTO> validator
              )
        {
            _customerFeedbackCaptionService = customerFeedbackCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _customerFeedbackCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] CustomerFeedbackCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _customerFeedbackCaptionService.CreateOrUpdate(dto);

            Log.Information("Müştəri rəyləri məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
