using CB.Application.DTOs.MissionCaption;
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
    public class MissionCaptionController : ControllerBase
    {
        private readonly IMissionCaptionService _missionCaptionService;
        private readonly IValidator<MissionCaptionPostDTO> _validator;

        public MissionCaptionController(
            IMissionCaptionService missionCaptionService,
             IValidator<MissionCaptionPostDTO> validator
              )
        {
            _missionCaptionService = missionCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _missionCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] MissionCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _missionCaptionService.CreateOrUpdate(dto);

            Log.Information("Missiya başlıq bölməsi məlumatlarının yenilənməsi uğurludur : {@Dto}", dto);

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
