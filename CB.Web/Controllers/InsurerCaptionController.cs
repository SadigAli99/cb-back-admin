using CB.Application.DTOs.InsurerCaption;
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
    public class InsurerCaptionController : ControllerBase
    {
        private readonly IInsurerCaptionService _insurerCaptionService;
        private readonly IValidator<InsurerCaptionPostDTO> _validator;

        public InsurerCaptionController(
            IInsurerCaptionService insurerCaptionService,
             IValidator<InsurerCaptionPostDTO> validator
              )
        {
            _insurerCaptionService = insurerCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _insurerCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] InsurerCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _insurerCaptionService.CreateOrUpdate(dto);

            Log.Information("Sığortaçı başlıq məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
