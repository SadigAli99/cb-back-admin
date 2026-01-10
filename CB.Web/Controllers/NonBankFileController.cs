using CB.Application.DTOs.NonBankFile;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.NonBankFile;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NonBankFileController : ControllerBase
    {
        private readonly INonBankFileService _nonBankFileService;
        private readonly NonBankFileCreateValidator _createValidator;
        private readonly NonBankFileEditValidator _editValidator;

        public NonBankFileController(
            INonBankFileService nonBankFileService,
            NonBankFileCreateValidator createValidator,
            NonBankFileEditValidator editValidator
        )
        {
            _nonBankFileService = nonBankFileService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<NonBankFileGetDTO> data = await _nonBankFileService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            NonBankFileGetDTO? data = await _nonBankFileService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("BOKT fayl məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] NonBankFileCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _nonBankFileService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Bank fayl məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Bank fayl məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] NonBankFileEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _nonBankFileService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("BOKT fayl məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("BOKT fayl məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _nonBankFileService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("BOKT fayl məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("BOKT fayl məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
