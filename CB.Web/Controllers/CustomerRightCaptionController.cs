using CB.Application.DTOs.CustomerRightCaption;
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
    public class CustomerRightCaptionController : ControllerBase
    {
        private readonly ICustomerRightCaptionService _customerRightCaptionService;
        private readonly IValidator<CustomerRightCaptionPostDTO> _validator;

        public CustomerRightCaptionController(
            ICustomerRightCaptionService customerRightCaptionService,
             IValidator<CustomerRightCaptionPostDTO> validator
              )
        {
            _customerRightCaptionService = customerRightCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _customerRightCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] CustomerRightCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _customerRightCaptionService.CreateOrUpdate(dto);

            Log.Information("Haqqımızda məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
