using CB.Application.DTOs.NationalBankNoteCaption;
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
    public class NationalBankNoteCaptionController : ControllerBase
    {
        private readonly INationalBankNoteCaptionService _nationalBankNoteCaptionService;
        private readonly IValidator<NationalBankNoteCaptionPostDTO> _validator;

        public NationalBankNoteCaptionController(
            INationalBankNoteCaptionService nationalBankNoteCaptionService,
             IValidator<NationalBankNoteCaptionPostDTO> validator
              )
        {
            _nationalBankNoteCaptionService = nationalBankNoteCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _nationalBankNoteCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] NationalBankNoteCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _nationalBankNoteCaptionService.CreateOrUpdate(dto);

            Log.Information("Milli pul nişanları məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
