using CB.Application.DTOs.CBAR100Video;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.CBAR100Video;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CBAR100VideoController : ControllerBase
    {
        private readonly ICBAR100VideoService _CBAR100VideoService;
        private readonly CBAR100VideoCreateValidator _createValidator;
        private readonly CBAR100VideoEditValidator _editValidator;

        public CBAR100VideoController(
            ICBAR100VideoService CBAR100VideoService,
            CBAR100VideoCreateValidator createValidator,
            CBAR100VideoEditValidator editValidator
        )
        {
            _CBAR100VideoService = CBAR100VideoService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<CBAR100VideoGetDTO> data = await _CBAR100VideoService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            CBAR100VideoGetDTO? data = await _CBAR100VideoService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("CBAR 100-cü il video məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CBAR100VideoCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _CBAR100VideoService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("CBAR 100-cü il video məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("CBAR 100-cü il video məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] CBAR100VideoEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _CBAR100VideoService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("CBAR 100-cü il video məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("CBAR 100-cü il video məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _CBAR100VideoService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("CBAR 100-cü il video məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("CBAR 100-cü il video məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
