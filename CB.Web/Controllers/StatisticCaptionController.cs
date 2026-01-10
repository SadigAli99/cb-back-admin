using CB.Application.DTOs.StatisticCaption;
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
    public class StatisticCaptionController : ControllerBase
    {
        private readonly IStatisticCaptionService _statisticCaptionService;
        private readonly IValidator<StatisticCaptionPostDTO> _validator;

        public StatisticCaptionController(
            IStatisticCaptionService statisticCaptionService,
             IValidator<StatisticCaptionPostDTO> validator
              )
        {
            _statisticCaptionService = statisticCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _statisticCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] StatisticCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _statisticCaptionService.CreateOrUpdate(dto);

            Log.Information("Statistika məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
