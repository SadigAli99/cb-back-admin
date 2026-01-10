using CB.Application.DTOs.Gallery;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.Gallery;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class GalleryController : ControllerBase
    {
        private readonly IGalleryService _galleryService;
        private readonly GalleryCreateValidator _createValidator;
        private readonly GalleryEditValidator _editValidator;

        public GalleryController(
            IGalleryService galleryService,
            GalleryCreateValidator createValidator,
            GalleryEditValidator editValidator
        )
        {
            _galleryService = galleryService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<GalleryGetDTO> data = await _galleryService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            GalleryGetDTO? data = await _galleryService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Qalereya məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] GalleryCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _galleryService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Qalereya məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Qalereya məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] GalleryEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _galleryService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Qalereya məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Qalereya məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _galleryService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Qalereya məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Qalereya məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

        [HttpDelete("{galleryId}/images/{imageId}")]
        public async Task<IActionResult> DeleteImage(int galleryId, int imageId)
        {
            var result = await _galleryService.DeleteImageAsync(galleryId, imageId);
            if (!result)
            {
                Log.Warning("Qalereya şəkli silinə bilmədi : GalleryId = {@GalleryId}, ImageId = {@ImageId}", galleryId, imageId);
                return NotFound(new { Message = "Şəkil tapılmadı və ya silinə bilmədi." });
            }

            Log.Warning("Qalereya şəkli uğurla silindi : GalleryId = {@GalleryId}, ImageId = {@ImageId}", galleryId, imageId);
            return Ok(new { Message = "Şəkil uğurla silindi." });
        }

    }
}
