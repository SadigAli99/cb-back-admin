using CB.Application.DTOs.PresidentDecree;
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
    public class PresidentDecreeController : ControllerBase
    {
        private readonly IPresidentDecreeService _presidentDecreeService;
        private readonly IValidator<PresidentDecreePostDTO> _validator;

        public PresidentDecreeController(
            IPresidentDecreeService presidentDecreeService,
             IValidator<PresidentDecreePostDTO> validator
              )
        {
            _presidentDecreeService = presidentDecreeService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _presidentDecreeService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] PresidentDecreePostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _presidentDecreeService.CreateOrUpdate(dto);

            Log.Information("Azərbaycan Respublikasının Prezindentinin fərmanı məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
