using CB.Application.DTOs.Inflation;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.Inflation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class InflationController : ControllerBase
    {
        private readonly IInflationService _inflationService;
        private readonly InflationCreateValidator _createValidator;
        private readonly InflationEditValidator _editValidator;

        public InflationController(
            IInflationService inflationService,
            InflationCreateValidator createValidator,
            InflationEditValidator editValidator
            )
        {
            _inflationService = inflationService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /Inflation
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _inflationService.GetAllAsync();
            return Ok(data);
        }

        // POST: /Inflation
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InflationCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool created = await _inflationService.CreateAsync(dto);

            if (!created)
            {
                Log.Warning("Elan məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dto);
                return BadRequest();
            }

            Log.Information("Elan məlumatı uğurla əlavə olundu : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // GET: /Inflation/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _inflationService.GetForEditAsync(id);
            if (data is null)
            {
                Log.Warning("Elan məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        // PUT: /Inflation/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] InflationEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(new
                {
                    Status = "error",
                    Messages = validationResult.Errors
                });
            }


            var updated = await _inflationService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Warning("Elan məlumatı yenilənməsi uğursuz oldu : {@Dto}", dto);
                return NotFound();
            }
            Log.Information("Elan məlumatı yenilənməsi uğurludur : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });

        }

        // DELETE: /Inflation/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _inflationService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Elan məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Elan məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }
    }
}
