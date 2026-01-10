using CB.Application.DTOs.ReviewApplicationLink;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.ReviewApplicationLink;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReviewApplicationLinkController : ControllerBase
    {
        private readonly IReviewApplicationLinkService _reviewApplicationLinkService;
        private readonly ReviewApplicationLinkCreateValidator _createValidator;
        private readonly ReviewApplicationLinkEditValidator _editValidator;

        public ReviewApplicationLinkController(
            IReviewApplicationLinkService reviewApplicationLinkService,
            ReviewApplicationLinkCreateValidator createValidator,
            ReviewApplicationLinkEditValidator editValidator
        )
        {
            _reviewApplicationLinkService = reviewApplicationLinkService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<ReviewApplicationLinkGetDTO> data = await _reviewApplicationLinkService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            ReviewApplicationLinkGetDTO? data = await _reviewApplicationLinkService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Vətəndaşların qeydiyyatı link məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReviewApplicationLinkCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _reviewApplicationLinkService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Vətəndaşların qeydiyyatı link məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Vətəndaşların qeydiyyatı link məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] ReviewApplicationLinkEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _reviewApplicationLinkService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Vətəndaşların qeydiyyatı link məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Vətəndaşların qeydiyyatı link məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _reviewApplicationLinkService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Vətəndaşların qeydiyyatı link məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Vətəndaşların qeydiyyatı link məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
