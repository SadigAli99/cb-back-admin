using CB.Application.DTOs.InternationalCooperationInitiative;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.InternationalCooperationInitiative;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InternationalCooperationInitiativeController : ControllerBase
    {
        private readonly IInternationalCooperationInitiativeService _internationalCooperationInitiativeService;
        private readonly InternationalCooperationInitiativeCreateValidator _createValidator;
        private readonly InternationalCooperationInitiativeEditValidator _editValidator;

        public InternationalCooperationInitiativeController(
            IInternationalCooperationInitiativeService internationalCooperationInitiativeService,
            InternationalCooperationInitiativeCreateValidator createValidator,
            InternationalCooperationInitiativeEditValidator editValidator
        )
        {
            _internationalCooperationInitiativeService = internationalCooperationInitiativeService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<InternationalCooperationInitiativeGetDTO> data = await _internationalCooperationInitiativeService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            InternationalCooperationInitiativeGetDTO? data = await _internationalCooperationInitiativeService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Beynəlxalq əməkdaşlıq və təşəbbüslər məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] InternationalCooperationInitiativeCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _internationalCooperationInitiativeService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Beynəlxalq əməkdaşlıq və təşəbbüslər məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Beynəlxalq əməkdaşlıq və təşəbbüslər məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] InternationalCooperationInitiativeEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _internationalCooperationInitiativeService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Beynəlxalq əməkdaşlıq və təşəbbüslər məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Beynəlxalq əməkdaşlıq və təşəbbüslər məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _internationalCooperationInitiativeService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Beynəlxalq əməkdaşlıq və təşəbbüslər məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Beynəlxalq əməkdaşlıq və təşəbbüslər məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
