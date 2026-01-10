using CB.Application.DTOs.RoadmapOverview;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.RoadmapOverview;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RoadmapOverviewController : ControllerBase
    {
        private readonly IRoadmapOverviewService _roadmapOverviewService;
        private readonly RoadmapOverviewCreateValidator _createValidator;
        private readonly RoadmapOverviewEditValidator _editValidator;

        public RoadmapOverviewController(
            IRoadmapOverviewService roadmapOverviewService,
            RoadmapOverviewCreateValidator createValidator,
            RoadmapOverviewEditValidator editValidator
        )
        {
            _roadmapOverviewService = roadmapOverviewService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RoadmapOverviewGetDTO> data = await _roadmapOverviewService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            RoadmapOverviewGetDTO? data = await _roadmapOverviewService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Azərbaycan Respublikası Mərkəzi Bankının 2024-2029-cu illəri əhatə edəcək Nəzarət Texnologiyaları üzrə Yol Xəritəsinin qısa icmalı məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] RoadmapOverviewCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _roadmapOverviewService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Azərbaycan Respublikası Mərkəzi Bankının 2024-2029-cu illəri əhatə edəcək Nəzarət Texnologiyaları üzrə Yol Xəritəsinin qısa icmalı məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Azərbaycan Respublikası Mərkəzi Bankının 2024-2029-cu illəri əhatə edəcək Nəzarət Texnologiyaları üzrə Yol Xəritəsinin qısa icmalı məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] RoadmapOverviewEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _roadmapOverviewService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Azərbaycan Respublikası Mərkəzi Bankının 2024-2029-cu illəri əhatə edəcək Nəzarət Texnologiyaları üzrə Yol Xəritəsinin qısa icmalı məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Azərbaycan Respublikası Mərkəzi Bankının 2024-2029-cu illəri əhatə edəcək Nəzarət Texnologiyaları üzrə Yol Xəritəsinin qısa icmalı məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _roadmapOverviewService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Azərbaycan Respublikası Mərkəzi Bankının 2024-2029-cu illəri əhatə edəcək Nəzarət Texnologiyaları üzrə Yol Xəritəsinin qısa icmalı məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Azərbaycan Respublikası Mərkəzi Bankının 2024-2029-cu illəri əhatə edəcək Nəzarət Texnologiyaları üzrə Yol Xəritəsinin qısa icmalı məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
