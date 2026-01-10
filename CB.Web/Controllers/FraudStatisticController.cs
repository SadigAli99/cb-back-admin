using CB.Application.DTOs.FraudStatistic;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.FraudStatistic;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FraudStatisticController : ControllerBase
    {
        private readonly IFraudStatisticService _fraudStatisticService;
        private readonly FraudStatisticCreateValidator _createValidator;
        private readonly FraudStatisticEditValidator _editValidator;

        public FraudStatisticController(
            IFraudStatisticService fraudStatisticService,
            FraudStatisticCreateValidator createValidator,
            FraudStatisticEditValidator editValidator
        )
        {
            _fraudStatisticService = fraudStatisticService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<FraudStatisticGetDTO> data = await _fraudStatisticService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            FraudStatisticGetDTO? data = await _fraudStatisticService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Fırıldaqçılıq statistikası məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] FraudStatisticCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _fraudStatisticService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Fırıldaqçılıq statistikası məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Fırıldaqçılıq statistikası məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] FraudStatisticEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _fraudStatisticService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Fırıldaqçılıq statistikası məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Fırıldaqçılıq statistikası məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _fraudStatisticService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Fırıldaqçılıq statistikası məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Fırıldaqçılıq statistikası məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
