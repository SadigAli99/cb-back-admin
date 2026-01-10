using CB.Application.DTOs.MonetaryPolicyCaption;
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
    public class MonetaryPolicyCaptionController : ControllerBase
    {
        private readonly IMonetaryPolicyCaptionService _monetaryPolicyCaptionService;
        private readonly IValidator<MonetaryPolicyCaptionPostDTO> _validator;

        public MonetaryPolicyCaptionController(
            IMonetaryPolicyCaptionService monetaryPolicyCaptionService,
             IValidator<MonetaryPolicyCaptionPostDTO> validator
              )
        {
            _monetaryPolicyCaptionService = monetaryPolicyCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _monetaryPolicyCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] MonetaryPolicyCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _monetaryPolicyCaptionService.CreateOrUpdate(dto);

            Log.Information("Pul siyasəti başlıq məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
