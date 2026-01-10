using CB.Application.DTOs.MonetaryIndicator;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.MonetaryIndicator;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class MonetaryIndicatorController : ControllerBase
    {
        private readonly IMonetaryIndicatorService _monetaryIndicatorService;
        private readonly MonetaryIndicatorCreateValidator _createValidator;
        private readonly MonetaryIndicatorEditValidator _editValidator;

        public MonetaryIndicatorController(
            IMonetaryIndicatorService monetaryIndicatorService,
            MonetaryIndicatorCreateValidator createValidator,
            MonetaryIndicatorEditValidator editValidator
            )
        {
            _monetaryIndicatorService = monetaryIndicatorService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /MonetaryIndicator
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _monetaryIndicatorService.GetAllAsync();
            return Ok(data);
        }

        // POST: /MonetaryIndicator
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MonetaryIndicatorCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool created = await _monetaryIndicatorService.CreateAsync(dto);
            if (!created)
            {
                Log.Warning("Monetar göstərici məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dto);
                return BadRequest();
            }

            Log.Information("Monetar göstərici məlumatı uğurla əlavə olundu : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // GET: /MonetaryIndicator/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _monetaryIndicatorService.GetForEditAsync(id);
            if (data is null)
            {
                Log.Warning("Monetar göstərici məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        // PUT : /MonetaryIndicator/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, MonetaryIndicatorEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            var updated = await _monetaryIndicatorService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Warning("Monetar göstərici məlumatı yenilənməsi uğursuz oldu : {@Dto}", dto);
                return NotFound();
            }
            Log.Information("Monetar göstərici məlumatı yenilənməsi uğurludur : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });

        }

        // DELETE: /MonetaryIndicator/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _monetaryIndicatorService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Monetar göstərici məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Monetar göstərici məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }
    }
}
