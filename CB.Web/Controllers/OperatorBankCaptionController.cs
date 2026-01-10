using CB.Application.DTOs.OperatorBankCaption;
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
    public class OperatorBankCaptionController : ControllerBase
    {
        private readonly IOperatorBankCaptionService _operatorBankCaptionService;
        private readonly IValidator<OperatorBankCaptionPostDTO> _validator;

        public OperatorBankCaptionController(
            IOperatorBankCaptionService operatorBankCaptionService,
             IValidator<OperatorBankCaptionPostDTO> validator
              )
        {
            _operatorBankCaptionService = operatorBankCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _operatorBankCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] OperatorBankCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _operatorBankCaptionService.CreateOrUpdate(dto);

            Log.Information("Haqqımızda məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
