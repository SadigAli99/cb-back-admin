using CB.Application.DTOs.InformationIssuerCaption;
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
    public class InformationIssuerCaptionController : ControllerBase
    {
        private readonly IInformationIssuerCaptionService _informationIssuerCaptionService;
        private readonly IValidator<InformationIssuerCaptionPostDTO> _validator;

        public InformationIssuerCaptionController(
            IInformationIssuerCaptionService informationIssuerCaptionService,
             IValidator<InformationIssuerCaptionPostDTO> validator
              )
        {
            _informationIssuerCaptionService = informationIssuerCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _informationIssuerCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] InformationIssuerCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _informationIssuerCaptionService.CreateOrUpdate(dto);

            Log.Information("Qiymət kağızlar haqqında məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
