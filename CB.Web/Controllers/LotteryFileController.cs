using CB.Application.DTOs.LotteryFile;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.LotteryFile;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class LotteryFileController : ControllerBase
    {
        private readonly ILotteryFileService _lotteryFileService;
        private readonly LotteryFileCreateValidator _createValidator;
        private readonly LotteryFileEditValidator _editValidator;

        public LotteryFileController(
            ILotteryFileService lotteryFileService,
            LotteryFileCreateValidator createValidator,
            LotteryFileEditValidator editValidator
        )
        {
            _lotteryFileService = lotteryFileService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/lotteryFile
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _lotteryFileService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/lotteryFile/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _lotteryFileService.GetByIdAsync(id);
            if (data == null)
            {
                Log.Warning("Lotoreya fayl məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }

            return Ok(new { status = "success", data });
        }

        // POST: /api/lotteryFile
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] LotteryFileCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _lotteryFileService.CreateAsync(dto);

            if (!created)
            {
                Log.Warning("Lotoreya fayl məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dto);
                return BadRequest();
            }
            Log.Information("Lotoreya fayl məlumatı uğurla əlavə olundu : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/lotteryFile/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] LotteryFileEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var updated = await _lotteryFileService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Warning("Lotoreya fayl məlumatı yenilənməsi uğursuz oldu : {@Dto}", dto);
                return NotFound();
            }
            Log.Information("Lotoreya fayl məlumatı yenilənməsi uğurludur : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        // DELETE: /api/lotteryFile/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _lotteryFileService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Lotoreya fayl məlumatının silinməsi uğursuzdur : Id = {@Id}", id);

                return NotFound();
            }

            Log.Information("Lotoreya fayl məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }
    }
}
