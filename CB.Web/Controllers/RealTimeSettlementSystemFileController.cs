using CB.Application.DTOs.RealTimeSettlementSystemFile;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.RealTimeSettlementSystemFile;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RealTimeSettlementSystemFileController : ControllerBase
    {
        private readonly IRealTimeSettlementSystemFileService _realTimeSettlementSystemFileService;
        private readonly RealTimeSettlementSystemFileCreateValidator _createValidator;
        private readonly RealTimeSettlementSystemFileEditValidator _editValidator;

        public RealTimeSettlementSystemFileController(
            IRealTimeSettlementSystemFileService realTimeSettlementSystemFileService,
            RealTimeSettlementSystemFileCreateValidator createValidator,
            RealTimeSettlementSystemFileEditValidator editValidator
        )
        {
            _realTimeSettlementSystemFileService = realTimeSettlementSystemFileService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RealTimeSettlementSystemFileGetDTO> data = await _realTimeSettlementSystemFileService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            RealTimeSettlementSystemFileGetDTO? data = await _realTimeSettlementSystemFileService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Real vaxt rejimində hesablaşmalar fayl məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] RealTimeSettlementSystemFileCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _realTimeSettlementSystemFileService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Real vaxt rejimində hesablaşmalar fayl məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Real vaxt rejimində hesablaşmalar fayl məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] RealTimeSettlementSystemFileEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _realTimeSettlementSystemFileService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Real vaxt rejimində hesablaşmalar fayl məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Real vaxt rejimində hesablaşmalar fayl məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _realTimeSettlementSystemFileService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Real vaxt rejimində hesablaşmalar fayl məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Real vaxt rejimində hesablaşmalar fayl məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
