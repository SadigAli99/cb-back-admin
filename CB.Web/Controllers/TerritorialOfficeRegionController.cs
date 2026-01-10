using CB.Application.DTOs.TerritorialOfficeRegion;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.TerritorialOfficeRegion;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TerritorialOfficeRegionController : ControllerBase
    {
        private readonly ITerritorialOfficeRegionService _territorialOfficeRegionService;
        private readonly TerritorialOfficeRegionCreateValidator _createValidator;
        private readonly TerritorialOfficeRegionEditValidator _editValidator;

        public TerritorialOfficeRegionController(
            ITerritorialOfficeRegionService territorialOfficeRegionService,
            TerritorialOfficeRegionCreateValidator createValidator,
            TerritorialOfficeRegionEditValidator editValidator
        )
        {
            _territorialOfficeRegionService = territorialOfficeRegionService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<TerritorialOfficeRegionGetDTO> data = await _territorialOfficeRegionService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            TerritorialOfficeRegionGetDTO? data = await _territorialOfficeRegionService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Regional ofis məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] TerritorialOfficeRegionCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _territorialOfficeRegionService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Regional ofis məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Regional ofis məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] TerritorialOfficeRegionEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _territorialOfficeRegionService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Regional ofis məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Regional ofis məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _territorialOfficeRegionService.DeleteAsync(id);

            if (!deleted)
            {
                Log.Warning("Regional ofis məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Regional ofis məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });


        }

    }
}
