using CB.Application.DTOs.BankCaption;
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
    public class BankCaptionController : ControllerBase
    {
        private readonly IBankCaptionService _bankCaptionService;
        private readonly IValidator<BankCaptionPostDTO> _validator;

        public BankCaptionController(
            IBankCaptionService bankCaptionService,
             IValidator<BankCaptionPostDTO> validator
              )
        {
            _bankCaptionService = bankCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _bankCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] BankCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _bankCaptionService.CreateOrUpdate(dto);

            Log.Information("Bank başlıq məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
