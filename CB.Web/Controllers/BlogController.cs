using CB.Application.DTOs.Blog;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.Blog;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly BlogCreateValidator _createValidator;
        private readonly BlogEditValidator _editValidator;

        public BlogController(
            IBlogService blogService,
            BlogCreateValidator createValidator,
            BlogEditValidator editValidator
        )
        {
            _blogService = blogService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<BlogGetDTO> data = await _blogService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            BlogGetDTO? data = await _blogService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Xəbər məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] BlogCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _blogService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Xəbər məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Xəbər məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] BlogEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _blogService.UpdateAsync(id, dTO);

            if (!updated)
            {
                Log.Warning("Xəbər məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Xəbər məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _blogService.DeleteAsync(id);

            if (!deleted)
            {
                Log.Warning("Xəbər məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Xəbər məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

        [HttpDelete("{blogId}/images/{imageId}")]
        public async Task<IActionResult> DeleteImage(int blogId, int imageId)
        {
            var result = await _blogService.DeleteImageAsync(blogId, imageId);
            if (!result)
            {
                Log.Warning("Xəbər şəkli silinə bilmədi : BlogId = {@BlogId}, ImageId = {@ImageId}", blogId, imageId);
                return NotFound(new { Message = "Şəkil tapılmadı və ya silinə bilmədi." });
            }
            Log.Information("Xəbər şəkli uğurla silindi : BlogId = {@BlogId}, ImageId = {@ImageId}", blogId, imageId);
            return Ok(new { Message = "Şəkil uğurla silindi." });
        }
    }
}
