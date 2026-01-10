using CB.Application.DTOs.RoadmapSustainableFinance;
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
    public class RoadmapSustainableFinanceController : ControllerBase
    {
        private readonly IRoadmapSustainableFinanceService _roadmapSustainableFinanceService;
        private readonly IValidator<RoadmapSustainableFinancePostDTO> _validator;

        public RoadmapSustainableFinanceController(
            IRoadmapSustainableFinanceService roadmapSustainableFinanceService,
             IValidator<RoadmapSustainableFinancePostDTO> validator
              )
        {
            _roadmapSustainableFinanceService = roadmapSustainableFinanceService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _roadmapSustainableFinanceService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] RoadmapSustainableFinancePostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _roadmapSustainableFinanceService.CreateOrUpdate(dto);

            Log.Information("Dayanıqlı maliyyə üzrə Yol Xəritəsi məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
