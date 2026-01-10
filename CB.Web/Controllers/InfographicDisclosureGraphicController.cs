using CB.Application.DTOs.InfographicDisclosureGraphic;
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
    public class InfographicDisclosureGraphicController : ControllerBase
    {
        private readonly IInfographicDisclosureGraphicService _infographicDisclosureGraphicService;
        private readonly IValidator<InfographicDisclosureGraphicPostDTO> _validator;

        public InfographicDisclosureGraphicController(
            IInfographicDisclosureGraphicService infographicDisclosureGraphicService,
             IValidator<InfographicDisclosureGraphicPostDTO> validator
              )
        {
            _infographicDisclosureGraphicService = infographicDisclosureGraphicService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _infographicDisclosureGraphicService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] InfographicDisclosureGraphicPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _infographicDisclosureGraphicService.CreateOrUpdate(dto);

            Log.Information("Məlumatların yayımlanması infoqrafiya məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
