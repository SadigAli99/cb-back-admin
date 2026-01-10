using CB.Application.DTOs.InsurerFile;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.InsurerFile;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InsurerFileController : ControllerBase
    {
        private readonly IInsurerFileService _insurerFileService;
        private readonly InsurerFileCreateValidator _createValidator;
        private readonly InsurerFileEditValidator _editValidator;

        public InsurerFileController(
            IInsurerFileService insurerFileService,
            InsurerFileCreateValidator createValidator,
            InsurerFileEditValidator editValidator
        )
        {
            _insurerFileService = insurerFileService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<InsurerFileGetDTO> data = await _insurerFileService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            InsurerFileGetDTO? data = await _insurerFileService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Sığortaçı fayl məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] InsurerFileCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _insurerFileService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Sığortaçı fayl məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Sığortaçı fayl məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] InsurerFileEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _insurerFileService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Sığortaçı fayl məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Sığortaçı fayl məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _insurerFileService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Sığortaçı fayl məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Sığortaçı fayl məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
