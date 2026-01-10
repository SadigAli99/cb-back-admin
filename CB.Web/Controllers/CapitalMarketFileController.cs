using CB.Application.DTOs.CapitalMarketFile;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.CapitalMarketFile;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CapitalMarketFileController : ControllerBase
    {
        private readonly ICapitalMarketFileService _capitalMarketFileService;
        private readonly CapitalMarketFileCreateValidator _createValidator;
        private readonly CapitalMarketFileEditValidator _editValidator;

        public CapitalMarketFileController(
            ICapitalMarketFileService capitalMarketFileService,
            CapitalMarketFileCreateValidator createValidator,
            CapitalMarketFileEditValidator editValidator
        )
        {
            _capitalMarketFileService = capitalMarketFileService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<CapitalMarketFileGetDTO> data = await _capitalMarketFileService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            CapitalMarketFileGetDTO? data = await _capitalMarketFileService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Kapital bazarı fayl məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CapitalMarketFileCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _capitalMarketFileService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Kapital bazarı fayl məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Kapital bazarı fayl məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] CapitalMarketFileEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _capitalMarketFileService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Kapital bazarı fayl məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Kapital bazarı fayl məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _capitalMarketFileService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Kapital bazarı fayl məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Kapital bazarı fayl məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
