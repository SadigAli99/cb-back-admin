using CB.Application.DTOs.RealTimeSettlementSystem;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.RealTimeSettlementSystem;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RealTimeSettlementSystemController : ControllerBase
    {
        private readonly IRealTimeSettlementSystemService _realTimeSettlementSystemService;
        private readonly RealTimeSettlementSystemCreateValidator _createValidator;
        private readonly RealTimeSettlementSystemEditValidator _editValidator;

        public RealTimeSettlementSystemController(
            IRealTimeSettlementSystemService realTimeSettlementSystemService,
            RealTimeSettlementSystemCreateValidator createValidator,
            RealTimeSettlementSystemEditValidator editValidator
        )
        {
            _realTimeSettlementSystemService = realTimeSettlementSystemService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RealTimeSettlementSystemGetDTO> data = await _realTimeSettlementSystemService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            RealTimeSettlementSystemGetDTO? data = await _realTimeSettlementSystemService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Real vaxt rejimində hesablaşmalar məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RealTimeSettlementSystemCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _realTimeSettlementSystemService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Real vaxt rejimində hesablaşmalar məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Real vaxt rejimində hesablaşmalar məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] RealTimeSettlementSystemEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _realTimeSettlementSystemService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Real vaxt rejimində hesablaşmalar məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Real vaxt rejimində hesablaşmalar məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _realTimeSettlementSystemService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Real vaxt rejimində hesablaşmalar məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Real vaxt rejimində hesablaşmalar məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
