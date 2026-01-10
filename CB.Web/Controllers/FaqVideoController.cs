using CB.Application.DTOs.FaqVideo;
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
    public class FaqVideoController : ControllerBase
    {
        private readonly IFaqVideoService _faqVideoService;
        private readonly IValidator<FaqVideoPostDTO> _validator;

        public FaqVideoController(
            IFaqVideoService faqVideoService,
             IValidator<FaqVideoPostDTO> validator
              )
        {
            _faqVideoService = faqVideoService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _faqVideoService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] FaqVideoPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _faqVideoService.CreateOrUpdate(dto);

            Log.Information("Tez-tez verilən suallar video məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
