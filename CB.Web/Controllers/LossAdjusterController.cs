using CB.Application.DTOs.LossAdjuster;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.LossAdjuster;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LossAdjusterController : ControllerBase
    {
        private readonly ILossAdjusterService _lossAdjusterService;
        private readonly LossAdjusterCreateValidator _createValidator;
        private readonly LossAdjusterEditValidator _editValidator;

        public LossAdjusterController(
            ILossAdjusterService lossAdjusterService,
            LossAdjusterCreateValidator createValidator,
            LossAdjusterEditValidator editValidator
        )
        {
            _lossAdjusterService = lossAdjusterService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<LossAdjusterGetDTO> data = await _lossAdjusterService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            LossAdjusterGetDTO? data = await _lossAdjusterService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Zərər tənzimləyicisi məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] LossAdjusterCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _lossAdjusterService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Zərər tənzimləyicisi məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Zərər tənzimləyicisi məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] LossAdjusterEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _lossAdjusterService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Zərər tənzimləyicisi məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Zərər tənzimləyicisi məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _lossAdjusterService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Zərər tənzimləyicisi məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Zərər tənzimləyicisi məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
