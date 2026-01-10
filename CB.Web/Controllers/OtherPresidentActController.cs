using CB.Application.DTOs.OtherPresidentAct;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.OtherPresidentAct;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OtherPresidentActController : ControllerBase
    {
        private readonly IOtherPresidentActService _otherPresidentActService;
        private readonly OtherPresidentActCreateValidator _createValidator;
        private readonly OtherPresidentActEditValidator _editValidator;

        public OtherPresidentActController(
            IOtherPresidentActService otherPresidentActService,
            OtherPresidentActCreateValidator createValidator,
            OtherPresidentActEditValidator editValidator
        )
        {
            _otherPresidentActService = otherPresidentActService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<OtherPresidentActGetDTO> data = await _otherPresidentActService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            OtherPresidentActGetDTO? data = await _otherPresidentActService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Digər AR prezidentinin aktları məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] OtherPresidentActCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _otherPresidentActService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Digər AR prezidentinin aktları məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Digər AR prezidentinin aktları məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] OtherPresidentActEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _otherPresidentActService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Digər AR prezidentinin aktları məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Digər AR prezidentinin aktları məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _otherPresidentActService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Digər AR prezidentinin aktları məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Digər AR prezidentinin aktları məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
