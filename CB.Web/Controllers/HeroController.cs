using CB.Application.DTOs.Hero;
using CB.Application.Interfaces.Services;
using CB.Shared.Extensions;
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
    public class HeroController : ControllerBase
    {
        private readonly IHeroService _heroService;
        private readonly IValidator<HeroPostDTO> _validator;
        private readonly IWebHostEnvironment _env;

        public HeroController(IHeroService heroService, IValidator<HeroPostDTO> validator, IWebHostEnvironment env)
        {
            _heroService = heroService;
            _validator = validator;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _heroService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromForm] HeroPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            if (dto.File != null)
            {
                var current = await _heroService.GetFirst();
                if (current != null && !string.IsNullOrEmpty(current.Image))
                {
                    FileManager.FileDelete(_env.WebRootPath, current.Image);
                }

                dto.Image = await dto.File.FileUpload(_env.WebRootPath, "heros");
            }

            await _heroService.CreateOrUpdate(dto);

            Log.Information("Hero bölməsi məlumatının yenilənməsi uğurludur : {@Dto}", dto);

            return Ok(new { Status = "success", Message = "Məlumat uğurla yeniləndi" });
        }
    }
}
