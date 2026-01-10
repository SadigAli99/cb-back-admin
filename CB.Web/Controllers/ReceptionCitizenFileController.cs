using CB.Application.DTOs.ReceptionCitizenFile;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.ReceptionCitizenFile;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReceptionCitizenFileController : ControllerBase
    {
        private readonly IReceptionCitizenFileService _receptionCitizenFileService;
        private readonly ReceptionCitizenFileCreateValidator _createValidator;
        private readonly ReceptionCitizenFileEditValidator _editValidator;

        public ReceptionCitizenFileController(
            IReceptionCitizenFileService receptionCitizenFileService,
            ReceptionCitizenFileCreateValidator createValidator,
            ReceptionCitizenFileEditValidator editValidator
        )
        {
            _receptionCitizenFileService = receptionCitizenFileService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<ReceptionCitizenFileGetDTO> data = await _receptionCitizenFileService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            ReceptionCitizenFileGetDTO? data = await _receptionCitizenFileService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Vətəndaşların qeydiyyatı fayl məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ReceptionCitizenFileCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _receptionCitizenFileService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Vətəndaşların qeydiyyatı fayl məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Vətəndaşların qeydiyyatı fayl məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] ReceptionCitizenFileEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _receptionCitizenFileService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Vətəndaşların qeydiyyatı fayl məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Vətəndaşların qeydiyyatı fayl məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _receptionCitizenFileService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Vətəndaşların qeydiyyatı fayl məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Vətəndaşların qeydiyyatı fayl məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
