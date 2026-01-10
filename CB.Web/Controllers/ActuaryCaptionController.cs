using CB.Application.DTOs.ActuaryCaption;
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
    public class ActuaryCaptionController : ControllerBase
    {
        private readonly IActuaryCaptionService _actuaryCaptionService;
        private readonly IValidator<ActuaryCaptionPostDTO> _validator;

        public ActuaryCaptionController(
            IActuaryCaptionService actuaryCaptionService,
             IValidator<ActuaryCaptionPostDTO> validator
              )
        {
            _actuaryCaptionService = actuaryCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _actuaryCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] ActuaryCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _actuaryCaptionService.CreateOrUpdate(dto);

            Log.Information("Aktuarilər haqqında məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
