using CB.Application.DTOs.ClearingSettlementSystemCaption;
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
    public class ClearingSettlementSystemCaptionController : ControllerBase
    {
        private readonly IClearingSettlementSystemCaptionService _clearingSettlementSystemCaptionService;
        private readonly IValidator<ClearingSettlementSystemCaptionPostDTO> _validator;

        public ClearingSettlementSystemCaptionController(
            IClearingSettlementSystemCaptionService clearingSettlementSystemCaptionService,
             IValidator<ClearingSettlementSystemCaptionPostDTO> validator
              )
        {
            _clearingSettlementSystemCaptionService = clearingSettlementSystemCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _clearingSettlementSystemCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] ClearingSettlementSystemCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _clearingSettlementSystemCaptionService.CreateOrUpdate(dto);

            Log.Information("Xırda ödənişlər üzrə kliriq hesablaşma sistemi məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
