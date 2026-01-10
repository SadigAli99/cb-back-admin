using CB.Application.DTOs.CentralBankCooperationCaption;
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
    public class CentralBankCooperationCaptionController : ControllerBase
    {
        private readonly ICentralBankCooperationCaptionService _centralBankCooperationCaptionService;
        private readonly IValidator<CentralBankCooperationCaptionPostDTO> _validator;

        public CentralBankCooperationCaptionController(
            ICentralBankCooperationCaptionService centralBankCooperationCaptionService,
             IValidator<CentralBankCooperationCaptionPostDTO> validator
              )
        {
            _centralBankCooperationCaptionService = centralBankCooperationCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _centralBankCooperationCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] CentralBankCooperationCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _centralBankCooperationCaptionService.CreateOrUpdate(dto);

            Log.Information("Mərkəzi bank əməkdaşlıqları məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
