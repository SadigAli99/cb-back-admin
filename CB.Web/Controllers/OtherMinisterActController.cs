using CB.Application.DTOs.OtherMinisterAct;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.OtherMinisterAct;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OtherMinisterActController : ControllerBase
    {
        private readonly IOtherMinisterActService _otherMinisterActService;
        private readonly OtherMinisterActCreateValidator _createValidator;
        private readonly OtherMinisterActEditValidator _editValidator;

        public OtherMinisterActController(
            IOtherMinisterActService otherMinisterActService,
            OtherMinisterActCreateValidator createValidator,
            OtherMinisterActEditValidator editValidator
        )
        {
            _otherMinisterActService = otherMinisterActService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<OtherMinisterActGetDTO> data = await _otherMinisterActService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            OtherMinisterActGetDTO? data = await _otherMinisterActService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Digər Nazirlər Kabinetinin aktları məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] OtherMinisterActCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _otherMinisterActService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Digər Nazirlər Kabinetinin aktları məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Digər Nazirlər Kabinetinin aktları məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] OtherMinisterActEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _otherMinisterActService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Digər Nazirlər Kabinetinin aktları məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Digər Nazirlər Kabinetinin aktları məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _otherMinisterActService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Digər Nazirlər Kabinetinin aktları məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Digər Nazirlər Kabinetinin aktları məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
