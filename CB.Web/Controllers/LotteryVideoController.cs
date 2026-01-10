using CB.Application.DTOs.LotteryVideo;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.LotteryVideo;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LotteryVideoController : ControllerBase
    {
        private readonly ILotteryVideoService _lotteryVideoService;
        private readonly LotteryVideoCreateValidator _createValidator;
        private readonly LotteryVideoEditValidator _editValidator;

        public LotteryVideoController(
            ILotteryVideoService lotteryVideoService,
            LotteryVideoCreateValidator createValidator,
            LotteryVideoEditValidator editValidator
        )
        {
            _lotteryVideoService = lotteryVideoService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int id)
        {
            List<LotteryVideoGetDTO> data = await _lotteryVideoService.GetAllAsync(id);
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            LotteryVideoGetDTO? data = await _lotteryVideoService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Lotoreya video məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LotteryVideoCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _lotteryVideoService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Lotoreya video məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Lotoreya video məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] LotteryVideoEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _lotteryVideoService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Lotoreya video məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Lotoreya video məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _lotteryVideoService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Lotoreya video məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Lotoreya video məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }

    }
}
