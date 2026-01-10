using CB.Application.DTOs.StaffArticleCaption;
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
    public class StaffArticleCaptionController : ControllerBase
    {
        private readonly IStaffArticleCaptionService _staffArticleCaptionService;
        private readonly IValidator<StaffArticleCaptionPostDTO> _validator;

        public StaffArticleCaptionController(
            IStaffArticleCaptionService staffArticleCaptionService,
             IValidator<StaffArticleCaptionPostDTO> validator
              )
        {
            _staffArticleCaptionService = staffArticleCaptionService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _staffArticleCaptionService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] StaffArticleCaptionPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _staffArticleCaptionService.CreateOrUpdate(dto);

            Log.Information("İşçi məqaləsi məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
