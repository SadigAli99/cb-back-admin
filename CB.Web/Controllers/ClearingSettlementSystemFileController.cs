using CB.Application.DTOs.ClearingSettlementSystemFile;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.ClearingSettlementSystemFile;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ClearingSettlementSystemFileController : ControllerBase
    {
        private readonly IClearingSettlementSystemFileService _clearingSettlementSystemFileService;
        private readonly ClearingSettlementSystemFileCreateValidator _createValidator;
        private readonly ClearingSettlementSystemFileEditValidator _editValidator;

        public ClearingSettlementSystemFileController(
            IClearingSettlementSystemFileService clearingSettlementSystemFileService,
            ClearingSettlementSystemFileCreateValidator createValidator,
            ClearingSettlementSystemFileEditValidator editValidator
        )
        {
            _clearingSettlementSystemFileService = clearingSettlementSystemFileService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<ClearingSettlementSystemFileGetDTO> data = await _clearingSettlementSystemFileService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            ClearingSettlementSystemFileGetDTO? data = await _clearingSettlementSystemFileService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Xırda ödənişlər üzrə kliriq hesablaşma sistemi fayl məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ClearingSettlementSystemFileCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _clearingSettlementSystemFileService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Xırda ödənişlər üzrə kliriq hesablaşma sistemi fayl məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Xırda ödənişlər üzrə kliriq hesablaşma sistemi fayl məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] ClearingSettlementSystemFileEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _clearingSettlementSystemFileService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Xırda ödənişlər üzrə kliriq hesablaşma sistemi fayl məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Xırda ödənişlər üzrə kliriq hesablaşma sistemi fayl məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _clearingSettlementSystemFileService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Xırda ödənişlər üzrə kliriq hesablaşma sistemi fayl məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Xırda ödənişlər üzrə kliriq hesablaşma sistemi fayl məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
