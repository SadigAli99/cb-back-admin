using CB.Application.DTOs.EconometricModelFile;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.EconometricModelFile;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EconometricModelFileController : ControllerBase
    {
        private readonly IEconometricModelFileService _econometricModelFileService;
        private readonly EconometricModelFileCreateValidator _createValidator;
        private readonly EconometricModelFileEditValidator _editValidator;

        public EconometricModelFileController(
            IEconometricModelFileService econometricModelFileService,
            EconometricModelFileCreateValidator createValidator,
            EconometricModelFileEditValidator editValidator
        )
        {
            _econometricModelFileService = econometricModelFileService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<EconometricModelFileGetDTO> data = await _econometricModelFileService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            EconometricModelFileGetDTO? data = await _econometricModelFileService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Ekonometrik model məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] EconometricModelFileCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _econometricModelFileService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Ekonometrik model məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Ekonometrik model məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] EconometricModelFileEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _econometricModelFileService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Ekonometrik model məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Ekonometrik model məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _econometricModelFileService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Ekonometrik model məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Ekonometrik model məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
