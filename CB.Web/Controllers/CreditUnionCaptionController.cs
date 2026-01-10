using CB.Application.DTOs.CreditUnionCaption;
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
    public class CreditUnionCaptionController : ControllerBase
    {
        private readonly ICreditUnionCaptionService _creditUniuonCaptionService;
        private readonly IValidator<CreditUnionCaptionPostDTO> _validator;

        public CreditUnionCaptionController(
            ICreditUnionCaptionService creditUniuonCaptionService,
             IValidator<CreditUnionCaptionPostDTO> validator
              )
        {
            _creditUniuonCaptionService = creditUniuonCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _creditUniuonCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] CreditUnionCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _creditUniuonCaptionService.CreateOrUpdate(dto);

            Log.Information("Kredit ittifaqları başlıq məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
