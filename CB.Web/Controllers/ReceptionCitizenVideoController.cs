using CB.Application.DTOs.ReceptionCitizenVideo;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.ReceptionCitizenVideo;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReceptionCitizenVideoController : ControllerBase
    {
        private readonly IReceptionCitizenVideoService _receptionCitizenVideoService;
        private readonly ReceptionCitizenVideoCreateValidator _createValidator;
        private readonly ReceptionCitizenVideoEditValidator _editValidator;

        public ReceptionCitizenVideoController(
            IReceptionCitizenVideoService receptionCitizenVideoService,
            ReceptionCitizenVideoCreateValidator createValidator,
            ReceptionCitizenVideoEditValidator editValidator
        )
        {
            _receptionCitizenVideoService = receptionCitizenVideoService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<ReceptionCitizenVideoGetDTO> data = await _receptionCitizenVideoService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            ReceptionCitizenVideoGetDTO? data = await _receptionCitizenVideoService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Vətəndaşların qeydiyyatı video məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReceptionCitizenVideoCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _receptionCitizenVideoService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Vətəndaşların qeydiyyatı video məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Vətəndaşların qeydiyyatı video məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] ReceptionCitizenVideoEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _receptionCitizenVideoService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Vətəndaşların qeydiyyatı video məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Vətəndaşların qeydiyyatı video məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _receptionCitizenVideoService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Vətəndaşların qeydiyyatı video məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Vətəndaşların qeydiyyatı video məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
