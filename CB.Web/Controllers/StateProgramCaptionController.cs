using CB.Application.DTOs.StateProgramCaption;
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
    public class StateProgramCaptionController : ControllerBase
    {
        private readonly IStateProgramCaptionService _stateProgramCaptionService;
        private readonly IValidator<StateProgramCaptionPostDTO> _validator;

        public StateProgramCaptionController(
            IStateProgramCaptionService stateProgramCaptionService,
             IValidator<StateProgramCaptionPostDTO> validator
              )
        {
            _stateProgramCaptionService = stateProgramCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _stateProgramCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] StateProgramCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _stateProgramCaptionService.CreateOrUpdate(dto);

            Log.Information("Dövlət proqramı məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
