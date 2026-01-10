using CB.Application.DTOs.TerritorialOfficeStatistic;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.TerritorialOfficeStatistic;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TerritorialOfficeStatisticController : ControllerBase
    {
        private readonly ITerritorialOfficeStatisticService _territorialOfficeStatisticService;
        private readonly TerritorialOfficeStatisticCreateValidator _createValidator;
        private readonly TerritorialOfficeStatisticEditValidator _editValidator;

        public TerritorialOfficeStatisticController(
            ITerritorialOfficeStatisticService territorialOfficeStatisticService,
            TerritorialOfficeStatisticCreateValidator createValidator,
            TerritorialOfficeStatisticEditValidator editValidator
        )
        {
            _territorialOfficeStatisticService = territorialOfficeStatisticService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<TerritorialOfficeStatisticGetDTO> data = await _territorialOfficeStatisticService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            TerritorialOfficeStatisticGetDTO? data = await _territorialOfficeStatisticService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Regional ofis statistika məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TerritorialOfficeStatisticCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _territorialOfficeStatisticService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Regional ofis statistika məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Regional ofis statistika məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] TerritorialOfficeStatisticEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _territorialOfficeStatisticService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Regional ofis statistika məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Regional ofis statistika məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _territorialOfficeStatisticService.DeleteAsync(id);

            if (!deleted)
            {
                Log.Warning("Regional ofis statistika məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Regional ofis statistika məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });


        }

    }
}
