using CB.Application.DTOs.PaperMoney;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.PaperMoney;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaperMoneyController : ControllerBase
    {
        private readonly IPaperMoneyService _paperMoneyService;
        private readonly PaperMoneyCreateValidator _createValidator;
        private readonly PaperMoneyEditValidator _editValidator;

        public PaperMoneyController(
            IPaperMoneyService paperMoneyService,
            PaperMoneyCreateValidator createValidator,
            PaperMoneyEditValidator editValidator
        )
        {
            _paperMoneyService = paperMoneyService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<PaperMoneyGetDTO> data = await _paperMoneyService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            PaperMoneyGetDTO? data = await _paperMoneyService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Kağız pul əskinasları məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] PaperMoneyCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _paperMoneyService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Kağız pul əskinasları məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Kağız pul əskinasları məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] PaperMoneyEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _paperMoneyService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Kağız pul əskinasları məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Kağız pul əskinasları məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _paperMoneyService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Kağız pul əskinasları məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Kağız pul əskinasları məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
