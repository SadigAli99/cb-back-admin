using CB.Application.DTOs.CBAR100Gallery;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.CBAR100Gallery;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CBAR100GalleryController : ControllerBase
    {
        private readonly ICBAR100GalleryService _CBAR100GalleryService;
        private readonly CBAR100GalleryCreateValidator _createValidator;
        private readonly CBAR100GalleryEditValidator _editValidator;

        public CBAR100GalleryController(
            ICBAR100GalleryService CBAR100GalleryService,
            CBAR100GalleryCreateValidator createValidator,
            CBAR100GalleryEditValidator editValidator
        )
        {
            _CBAR100GalleryService = CBAR100GalleryService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<CBAR100GalleryGetDTO> data = await _CBAR100GalleryService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            CBAR100GalleryGetDTO? data = await _CBAR100GalleryService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("CBAR 100-cü il qalareya məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CBAR100GalleryCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _CBAR100GalleryService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("CBAR 100-cü il qalareya məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("CBAR 100-cü il qalareya məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] CBAR100GalleryEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _CBAR100GalleryService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("CBAR 100-cü il qalareya məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("CBAR 100-cü il qalareya məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _CBAR100GalleryService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("CBAR 100-cü il qalareya məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("CBAR 100-cü il qalareya məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
