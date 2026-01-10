using CB.Application.DTOs.OtherInfo;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.OtherInfo;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OtherInfoController : ControllerBase
    {
        private readonly IOtherInfoService _otherInfoService;
        private readonly OtherInfoCreateValidator _createValidator;
        private readonly OtherInfoEditValidator _editValidator;

        public OtherInfoController(
            IOtherInfoService otherInfoService,
            OtherInfoCreateValidator createValidator,
            OtherInfoEditValidator editValidator
        )
        {
            _otherInfoService = otherInfoService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<OtherInfoGetDTO> data = await _otherInfoService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            OtherInfoGetDTO? data = await _otherInfoService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Digər məlumat bölməsi məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OtherInfoCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _otherInfoService.CreateAsync(dTO);

            if (!created)
            {
                Log.Warning("Digər məlumat bölməsi məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Digər məlumat bölməsi məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] OtherInfoEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _otherInfoService.UpdateAsync(id, dTO);

            if (!updated)
            {
                Log.Warning("Digər məlumat bölməsi məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Digər məlumat bölməsi məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _otherInfoService.DeleteAsync(id);

            if (!deleted)
            {
                Log.Warning("Digər məlumat bölməsi məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Digər məlumat bölməsi məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
