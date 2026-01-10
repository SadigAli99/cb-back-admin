using CB.Application.DTOs.Poster;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.Poster;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class PosterController : ControllerBase
    {
        private readonly IPosterService _addressService;
        private readonly PosterCreateValidator _createValidator;
        private readonly PosterEditValidator _editValidator;

        public PosterController(
            IPosterService addressService,
            PosterCreateValidator createValidator,
            PosterEditValidator editValidator
        )
        {
            _addressService = addressService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/address
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _addressService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/address/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _addressService.GetByIdAsync(id);
            if (data == null)
            {
                Log.Warning("Poster məlumatı tapılmadı. Id={Id}", id);
                return NotFound();
            }

            return Ok(data);
        }

        // POST: /api/address
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] PosterCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _addressService.CreateAsync(dto);

            if (!created)
            {
                Log.Error("Poster əlavə edilməsi uğursuz oldu {@Dto}", dto);
                return BadRequest(new { status = "error", message = "Məlumat əlavə olunmadı" });
            }

            Log.Information("Məlumat uğurla əlavə olundu {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/address/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] PosterEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });

            var updated = await _addressService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Error("Poster yenilənməsi uğursuz oldu {@Dto}", dto);
                return NotFound();
            }

            Log.Information("Poster yenilənməsi uğurludur {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        // DELETE: /api/address/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _addressService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Poster silinməsi uğursuz oldu Id={@Id}", id);
                return NotFound();
            }
            Log.Information("Poster uğurla silindi : Id={@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }
    }
}
