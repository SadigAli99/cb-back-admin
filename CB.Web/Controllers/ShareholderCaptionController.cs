using CB.Application.DTOs.ShareholderCaption;
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
    public class ShareholderCaptionController : ControllerBase
    {
        private readonly IShareholderCaptionService _shareholderCaptionService;
        private readonly IValidator<ShareholderCaptionPostDTO> _validator;

        public ShareholderCaptionController(
            IShareholderCaptionService shareholderCaptionService,
             IValidator<ShareholderCaptionPostDTO> validator
              )
        {
            _shareholderCaptionService = shareholderCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _shareholderCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] ShareholderCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _shareholderCaptionService.CreateOrUpdate(dto);

            Log.Information("Səhmdar məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
