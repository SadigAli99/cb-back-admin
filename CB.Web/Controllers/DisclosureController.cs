using CB.Application.DTOs.Disclosure;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.Disclosure;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DisclosureController : ControllerBase
    {
        private readonly IDisclosureService _disclosureService;
        private readonly DisclosureCreateValidator _createValidator;
        private readonly DisclosureEditValidator _editValidator;

        public DisclosureController(
            IDisclosureService disclosureService,
            DisclosureCreateValidator createValidator,
            DisclosureEditValidator editValidator
        )
        {
            _disclosureService = disclosureService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<DisclosureGetDTO> data = await _disclosureService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            DisclosureGetDTO? data = await _disclosureService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Açıqlama məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] DisclosureCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _disclosureService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Açıqlama məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Açıqlama məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] DisclosureEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _disclosureService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Açıqlama məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Açıqlama məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _disclosureService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Açıqlama məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Açıqlama məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
