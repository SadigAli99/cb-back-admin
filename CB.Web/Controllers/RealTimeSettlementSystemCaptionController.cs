using CB.Application.DTOs.RealTimeSettlementSystemCaption;
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
    public class RealTimeSettlementSystemCaptionController : ControllerBase
    {
        private readonly IRealTimeSettlementSystemCaptionService _realTimeSettlementSystemCaptionService;
        private readonly IValidator<RealTimeSettlementSystemCaptionPostDTO> _validator;

        public RealTimeSettlementSystemCaptionController(
            IRealTimeSettlementSystemCaptionService realTimeSettlementSystemCaptionService,
             IValidator<RealTimeSettlementSystemCaptionPostDTO> validator
              )
        {
            _realTimeSettlementSystemCaptionService = realTimeSettlementSystemCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _realTimeSettlementSystemCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] RealTimeSettlementSystemCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _realTimeSettlementSystemCaptionService.CreateOrUpdate(dto);

            Log.Information("Haqqımızda məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
