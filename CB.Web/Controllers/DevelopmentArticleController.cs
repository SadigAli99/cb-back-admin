using CB.Application.DTOs.DevelopmentArticle;
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
    public class DevelopmentArticleController : ControllerBase
    {
        private readonly IDevelopmentArticleService _developmentArticleService;
        private readonly IValidator<DevelopmentArticlePostDTO> _validator;

        public DevelopmentArticleController(
            IDevelopmentArticleService developmentArticleService,
             IValidator<DevelopmentArticlePostDTO> validator
              )
        {
            _developmentArticleService = developmentArticleService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _developmentArticleService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] DevelopmentArticlePostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _developmentArticleService.CreateOrUpdate(dto);

            Log.Information("İnkişaf məqaləsi məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
