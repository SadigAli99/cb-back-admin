using CB.Application.DTOs.NonBankCaption;
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
    public class NonBankCaptionController : ControllerBase
    {
        private readonly INonBankCaptionService _nonBankCaptionService;
        private readonly IValidator<NonBankCaptionPostDTO> _validator;

        public NonBankCaptionController(
            INonBankCaptionService nonBankCaptionService,
             IValidator<NonBankCaptionPostDTO> validator
              )
        {
            _nonBankCaptionService = nonBankCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _nonBankCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] NonBankCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _nonBankCaptionService.CreateOrUpdate(dto);

            Log.Information("BOKT başlıq məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
