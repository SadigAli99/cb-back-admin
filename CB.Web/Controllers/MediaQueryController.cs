using CB.Application.DTOs.MediaQuery;
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
    public class MediaQueryController : ControllerBase
    {
        private readonly IMediaQueryService _mediaQueryService;
        private readonly IValidator<MediaQueryPostDTO> _validator;

        public MediaQueryController(
            IMediaQueryService mediaQueryService,
             IValidator<MediaQueryPostDTO> validator
              )
        {
            _mediaQueryService = mediaQueryService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _mediaQueryService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] MediaQueryPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _mediaQueryService.CreateOrUpdate(dto);

            Log.Information("Media sorğuları məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
