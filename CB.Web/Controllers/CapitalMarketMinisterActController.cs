using CB.Application.DTOs.CapitalMarketMinisterAct;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.CapitalMarketMinisterAct;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CapitalMarketMinisterActController : ControllerBase
    {
        private readonly ICapitalMarketMinisterActService _capitalMarketMinisterActService;
        private readonly CapitalMarketMinisterActCreateValidator _createValidator;
        private readonly CapitalMarketMinisterActEditValidator _editValidator;

        public CapitalMarketMinisterActController(
            ICapitalMarketMinisterActService capitalMarketMinisterActService,
            CapitalMarketMinisterActCreateValidator createValidator,
            CapitalMarketMinisterActEditValidator editValidator
        )
        {
            _capitalMarketMinisterActService = capitalMarketMinisterActService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<CapitalMarketMinisterActGetDTO> data = await _capitalMarketMinisterActService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            CapitalMarketMinisterActGetDTO? data = await _capitalMarketMinisterActService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Kapital bazarı Nazirlər Kabinetinin aktları məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CapitalMarketMinisterActCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _capitalMarketMinisterActService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Kapital bazarı Nazirlər Kabinetinin aktları məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Kapital bazarı Nazirlər Kabinetinin aktları məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] CapitalMarketMinisterActEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _capitalMarketMinisterActService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Kapital bazarı Nazirlər Kabinetinin aktları məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Kapital bazarı Nazirlər Kabinetinin aktları məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _capitalMarketMinisterActService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Kapital bazarı Nazirlər Kabinetinin aktları məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Kapital bazarı Nazirlər Kabinetinin aktları məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
