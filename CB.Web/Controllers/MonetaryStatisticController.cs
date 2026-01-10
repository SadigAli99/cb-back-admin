using CB.Application.DTOs.MonetaryStatistic;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.MonetaryStatistic;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MonetaryStatisticController : ControllerBase
    {
        private readonly IMonetaryStatisticService _monetaryStatisticService;
        private readonly MonetaryStatisticCreateValidator _createValidator;
        private readonly MonetaryStatisticEditValidator _editValidator;

        public MonetaryStatisticController(
            IMonetaryStatisticService monetaryStatisticService,
            MonetaryStatisticCreateValidator createValidator,
            MonetaryStatisticEditValidator editValidator
        )
        {
            _monetaryStatisticService = monetaryStatisticService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<MonetaryStatisticGetDTO> data = await _monetaryStatisticService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            MonetaryStatisticGetDTO? data = await _monetaryStatisticService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Pul-kredit statistika məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] MonetaryStatisticCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _monetaryStatisticService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Pul-kredit statistika məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Pul-kredit statistika məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] MonetaryStatisticEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _monetaryStatisticService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Pul-kredit statistika məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Pul-kredit statistika məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _monetaryStatisticService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Pul-kredit statistika məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Pul-kredit statistika məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
