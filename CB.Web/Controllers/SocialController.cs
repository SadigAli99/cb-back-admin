using CB.Application.DTOs.Social;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.Social;
using CB.Shared.Extensions;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class SocialController : ControllerBase
    {
        private readonly ISocialService _socialService;
        private readonly SocialCreateValidator _createValidator;
        private readonly SocialEditValidator _editValidator;
        private readonly IWebHostEnvironment _env;

        public SocialController(
            ISocialService socialService,
            SocialCreateValidator createValidator,
            SocialEditValidator editValidator,
            IWebHostEnvironment env
        )
        {
            _socialService = socialService;
            _createValidator = createValidator;
            _editValidator = editValidator;
            _env = env;
        }

        // GET: /api/social
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _socialService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/social/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _socialService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Sosial şəbəkə məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        // POST: /api/social
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] SocialCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            dto.Icon = await dto.File.FileUpload(_env.WebRootPath, "socials");
            var created = await _socialService.CreateAsync(dto);
            if (!created)
            {
                Log.Warning("Sosial şəbəkə məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dto);
                return BadRequest();
            }

            Log.Information("Sosial şəbəkə məlumatı uğurla əlavə olundu : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/social/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] SocialEditDTO dto)
        {
            var social = await _socialService.GetByIdAsync(id);
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            if (dto.File != null)
            {
                dto.Icon = await dto.File.FileUpload(_env.WebRootPath, "socials");
                FileManager.FileDelete(_env.WebRootPath, social?.Icon ?? "");
            }

            var updated = await _socialService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Warning("Sosial şəbəkə məlumatı yenilənməsi uğursuz oldu : {@Dto}", dto);
                return NotFound();
            }
            Log.Information("Sosial şəbəkə məlumatı yenilənməsi uğurludur : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });

        }

        // DELETE: /api/social/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _socialService.GetByIdAsync(id);
            FileManager.FileDelete(_env.WebRootPath, item?.Icon ?? "");
            var deleted = await _socialService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Sosial şəbəkə məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Sosial şəbəkə məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }
    }
}
