using CB.Application.DTOs.ReceptionCitizen;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.ReceptionCitizen;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReceptionCitizenController : ControllerBase
    {
        private readonly IReceptionCitizenService _receptionCitizenService;
        private readonly ReceptionCitizenCreateValidator _createValidator;
        private readonly ReceptionCitizenEditValidator _editValidator;

        public ReceptionCitizenController(
            IReceptionCitizenService receptionCitizenService,
            ReceptionCitizenCreateValidator createValidator,
            ReceptionCitizenEditValidator editValidator
        )
        {
            _receptionCitizenService = receptionCitizenService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<ReceptionCitizenGetDTO> data = await _receptionCitizenService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            ReceptionCitizenGetDTO? data = await _receptionCitizenService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Vətəndaşların qeydiyyatı məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReceptionCitizenCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _receptionCitizenService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Vətəndaşların qeydiyyatı məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Vətəndaşların qeydiyyatı məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] ReceptionCitizenEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _receptionCitizenService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Vətəndaşların qeydiyyatı məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Vətəndaşların qeydiyyatı məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _receptionCitizenService.DeleteAsync(id);

            if (!deleted)
            {
                Log.Warning("Vətəndaşların qeydiyyatı məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Vətəndaşların qeydiyyatı məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });


        }

    }
}
