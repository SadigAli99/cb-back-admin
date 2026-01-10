using CB.Application.DTOs.CareerCaption;
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
    public class CareerCaptionController : ControllerBase
    {
        private readonly ICareerCaptionService _careerCaptionService;
        private readonly IValidator<CareerCaptionPostDTO> _validator;

        public CareerCaptionController(
            ICareerCaptionService careerCaptionService,
             IValidator<CareerCaptionPostDTO> validator
              )
        {
            _careerCaptionService = careerCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _careerCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] CareerCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _careerCaptionService.CreateOrUpdate(dto);

            Log.Information("Karyera məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
