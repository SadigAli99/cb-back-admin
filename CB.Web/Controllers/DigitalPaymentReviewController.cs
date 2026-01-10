using CB.Application.DTOs.DigitalPaymentReview;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.DigitalPaymentReview;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DigitalPaymentReviewController : ControllerBase
    {
        private readonly IDigitalPaymentReviewService _digitalPaymentReviewService;
        private readonly DigitalPaymentReviewCreateValidator _createValidator;
        private readonly DigitalPaymentReviewEditValidator _editValidator;

        public DigitalPaymentReviewController(
            IDigitalPaymentReviewService digitalPaymentReviewService,
            DigitalPaymentReviewCreateValidator createValidator,
            DigitalPaymentReviewEditValidator editValidator
        )
        {
            _digitalPaymentReviewService = digitalPaymentReviewService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<DigitalPaymentReviewGetDTO> data = await _digitalPaymentReviewService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            DigitalPaymentReviewGetDTO? data = await _digitalPaymentReviewService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Rəqəmsal ödəniş icmalı məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] DigitalPaymentReviewCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _digitalPaymentReviewService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Rəqəmsal ödəniş icmalı məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Rəqəmsal ödəniş icmalı məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] DigitalPaymentReviewEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _digitalPaymentReviewService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Rəqəmsal ödəniş icmalı məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Rəqəmsal ödəniş icmalı məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _digitalPaymentReviewService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Rəqəmsal ödəniş icmalı məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Rəqəmsal ödəniş icmalı məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
