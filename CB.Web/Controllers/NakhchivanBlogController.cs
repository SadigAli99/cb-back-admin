using CB.Application.DTOs.NakhchivanBlog;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.NakhchivanBlog;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NakhchivanBlogController : ControllerBase
    {
        private readonly INakhchivanBlogService _nakhchivanBlogService;
        private readonly NakhchivanBlogCreateValidator _createValidator;
        private readonly NakhchivanBlogEditValidator _editValidator;

        public NakhchivanBlogController(
            INakhchivanBlogService nakhchivanBlogService,
            NakhchivanBlogCreateValidator createValidator,
            NakhchivanBlogEditValidator editValidator
        )
        {
            _nakhchivanBlogService = nakhchivanBlogService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<NakhchivanBlogGetDTO> data = await _nakhchivanBlogService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            NakhchivanBlogGetDTO? data = await _nakhchivanBlogService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Naxçıvan bloq məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] NakhchivanBlogCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _nakhchivanBlogService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Naxçıvan bloq məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Naxçıvan bloq məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] NakhchivanBlogEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _nakhchivanBlogService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Naxçıvan bloq məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Naxçıvan bloq məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _nakhchivanBlogService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Naxçıvan bloq məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Naxçıvan bloq məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

        [HttpDelete("{nakhchivanBlogId}/images/{imageId}")]
        public async Task<IActionResult> DeleteImage(int nakhchivanBlogId, int imageId)
        {
            var result = await _nakhchivanBlogService.DeleteImageAsync(nakhchivanBlogId, imageId);
            if (!result)
            {
                Log.Warning("Naxçıvan bloq şəkli silinə bilmədi : nakhchivanBlogId = {@nakhchivanBlogId}, ImageId = {@ImageId}", nakhchivanBlogId, imageId);
                return NotFound(new { Message = "Şəkil tapılmadı və ya silinə bilmədi." });
            }

            Log.Warning("Naxçıvan bloq şəkli uğurla silindi : nakhchivanBlogId = {@nakhchivanBlogId}, ImageId = {@ImageId}", nakhchivanBlogId, imageId);
            return Ok(new { Message = "Şəkil uğurla silindi." });
        }

    }
}
