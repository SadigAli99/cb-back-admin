using CB.Application.DTOs.Page;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.Page;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class PageController : ControllerBase
    {
        private readonly IPageService _pageService;
        private readonly PageCreateValidator _createValidator;
        private readonly PageEditValidator _editValidator;

        public PageController(
            IPageService pageService,
            PageCreateValidator createValidator,
            PageEditValidator editValidator
        )
        {
            _pageService = pageService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/page
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _pageService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/page/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _pageService.GetByIdAsync(id);
            if (data == null)
            {
                Log.Warning("Səhifə məlumatı tapılmadı. Id={Id}", id);
                return NotFound();
            }

            return Ok(data);
        }

        // POST: /api/page
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] PageCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _pageService.CreateAsync(dto);

            if (!created)
            {
                Log.Error("Səhifə əlavə edilməsi uğursuz oldu {@Dto}", dto);
                return BadRequest(new { status = "error", message = "Məlumat əlavə olunmadı" });
            }

            Log.Information("Məlumat uğurla əlavə olundu {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/page/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] PageEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });

            var updated = await _pageService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Error("Səhifə yenilənməsi uğursuz oldu {@Dto}", dto);
                return NotFound();
            }

            Log.Information("Səhifə yenilənməsi uğurludur {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        // DELETE: /api/page/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _pageService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Səhifə silinməsi uğursuz oldu Id={@Id}", id);
                return NotFound();
            }
            Log.Information("Səhifə uğurla silindi : Id={@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }
    }
}
