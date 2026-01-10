using CB.Application.DTOs.CreditInstitutionRightCaption;
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
    public class CreditInstitutionRightCaptionController : ControllerBase
    {
        private readonly ICreditInstitutionRightCaptionService _creditInstitutionRightCaptionService;
        private readonly IValidator<CreditInstitutionRightCaptionPostDTO> _validator;

        public CreditInstitutionRightCaptionController(
            ICreditInstitutionRightCaptionService creditInstitutionRightCaptionService,
             IValidator<CreditInstitutionRightCaptionPostDTO> validator
              )
        {
            _creditInstitutionRightCaptionService = creditInstitutionRightCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _creditInstitutionRightCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] CreditInstitutionRightCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _creditInstitutionRightCaptionService.CreateOrUpdate(dto);

            Log.Information("Kredit təşkilatı qanun məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
