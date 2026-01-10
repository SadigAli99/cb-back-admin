using CB.Application.DTOs.MonetaryPolicyReview;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.MonetaryPolicyReview;
using CB.Shared.Helpers;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class MonetaryPolicyReviewController : ControllerBase
    {
        private readonly IMonetaryPolicyReviewService _monetaryPolicyReviewService;
        private readonly MonetaryPolicyReviewCreateValidator _createValidator;
        private readonly MonetaryPolicyReviewEditValidator _editValidator;

        public MonetaryPolicyReviewController(
            IMonetaryPolicyReviewService monetaryPolicyReviewService,
            MonetaryPolicyReviewCreateValidator createValidator,
            MonetaryPolicyReviewEditValidator editValidator
        )
        {
            _monetaryPolicyReviewService = monetaryPolicyReviewService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/monetaryPolicyReview
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _monetaryPolicyReviewService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/monetaryPolicyReview/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _monetaryPolicyReviewService.GetByIdAsync(id);
            if (data == null)
            {
                Log.Warning("Pul siyasəti icmal məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }

            return Ok(new { status = "success", data });
        }

        // POST: /api/monetaryPolicyReview
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] MonetaryPolicyReviewCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _monetaryPolicyReviewService.CreateAsync(dto);

            if (!created)
            {
                Log.Warning("Pul siyasəti icmal məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dto);
                return BadRequest();
            }
            Log.Information("Pul siyasəti icmal məlumatı uğurla əlavə olundu : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/monetaryPolicyReview/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] MonetaryPolicyReviewEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var updated = await _monetaryPolicyReviewService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Warning("Pul siyasəti icmal məlumatı yenilənməsi uğursuz oldu : {@Dto}", dto);
                return NotFound();
            }
            Log.Information("Pul siyasəti icmal məlumatı yenilənməsi uğurludur : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        // DELETE: /api/monetaryPolicyReview/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _monetaryPolicyReviewService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Pul siyasəti icmal məlumatının silinməsi uğursuzdur : Id = {@Id}", id);

                return NotFound();
            }

            Log.Information("Pul siyasəti icmal məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }
    }
}
