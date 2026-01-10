using CB.Application.DTOs.InformationMemorandumCaption;
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
    public class InformationMemorandumCaptionController : ControllerBase
    {
        private readonly IInformationMemorandumCaptionService _informationIssuerCaptionService;
        private readonly IValidator<InformationMemorandumCaptionPostDTO> _validator;

        public InformationMemorandumCaptionController(
            IInformationMemorandumCaptionService informationIssuerCaptionService,
             IValidator<InformationMemorandumCaptionPostDTO> validator
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
        public async Task<IActionResult> Submit([FromBody] InformationMemorandumCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _informationIssuerCaptionService.CreateOrUpdate(dto);

            Log.Information("İnformasiya memorandumu haqqında məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
