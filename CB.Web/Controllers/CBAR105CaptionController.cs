using CB.Application.DTOs.CBAR105Caption;
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
    public class CBAR105CaptionController : ControllerBase
    {
        private readonly ICBAR105CaptionService _CBAR105CaptionService;
        private readonly IValidator<CBAR105CaptionPostDTO> _validator;

        public CBAR105CaptionController(
            ICBAR105CaptionService CBAR105CaptionService,
             IValidator<CBAR105CaptionPostDTO> validator
              )
        {
            _CBAR105CaptionService = CBAR105CaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _CBAR105CaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] CBAR105CaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _CBAR105CaptionService.CreateOrUpdate(dto);

            Log.Information("CBAR 105-ci il məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
