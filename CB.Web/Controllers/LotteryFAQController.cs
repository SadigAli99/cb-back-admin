using CB.Application.DTOs.LotteryFAQ;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.LotteryFAQ;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LotteryFAQController : ControllerBase
    {
        private readonly ILotteryFAQService _lotteryFAQService;
        private readonly LotteryFAQCreateValidator _createValidator;
        private readonly LotteryFAQEditValidator _editValidator;

        public LotteryFAQController(
            ILotteryFAQService lotteryFAQService,
            LotteryFAQCreateValidator createValidator,
            LotteryFAQEditValidator editValidator
        )
        {
            _lotteryFAQService = lotteryFAQService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int id)
        {
            List<LotteryFAQGetDTO> data = await _lotteryFAQService.GetAllAsync(id);
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            LotteryFAQGetDTO? data = await _lotteryFAQService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Lotoreya FAQ məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LotteryFAQCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _lotteryFAQService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Lotoreya FAQ məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Lotoreya FAQ məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] LotteryFAQEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _lotteryFAQService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Lotoreya FAQ məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Lotoreya FAQ məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _lotteryFAQService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Lotoreya FAQ məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Lotoreya FAQ məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }

    }
}
