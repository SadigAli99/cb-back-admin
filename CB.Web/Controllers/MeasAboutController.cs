using CB.Application.DTOs.MeasAbout;
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
    public class MeasAboutController : ControllerBase
    {
        private readonly IMeasAboutService _measAboutService;
        private readonly IValidator<MeasAboutPostDTO> _validator;

        public MeasAboutController(
            IMeasAboutService measAboutService,
             IValidator<MeasAboutPostDTO> validator
              )
        {
            _measAboutService = measAboutService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _measAboutService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] MeasAboutPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _measAboutService.CreateOrUpdate(dto);

            Log.Information("Meas məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
