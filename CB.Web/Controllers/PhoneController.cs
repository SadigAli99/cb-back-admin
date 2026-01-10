using CB.Application.DTOs.Phone;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.Phone;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class PhoneController : ControllerBase
    {
        private readonly IPhoneService _phoneService;
        private readonly PhoneCreateValidator _createValidator;
        private readonly PhoneEditValidator _editValidator;

        public PhoneController(
            IPhoneService phoneService,
            PhoneCreateValidator createValidator,
            PhoneEditValidator editValidator
        )
        {
            _phoneService = phoneService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/phone
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _phoneService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/phone/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _phoneService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Əlaqə nömrəsi məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        // POST: /api/phone
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PhoneCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _phoneService.CreateAsync(dto);

            if (!created)
            {
                Log.Warning("Əlaqə nömrəsi məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dto);
                return BadRequest();
            }

            Log.Information("Əlaqə nömrəsi məlumatı uğurla əlavə olundu : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/phone/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] PhoneEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });

            var updated = await _phoneService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Warning("Əlaqə nömrəsi məlumatı yenilənməsi uğursuz oldu : {@Dto}", dto);
                return NotFound();
            }
            Log.Information("Əlaqə nömrəsi məlumatı yenilənməsi uğurludur : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });

        }

        // DELETE: /api/phone/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _phoneService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Əlaqə nömrəsi məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Əlaqə nömrəsi məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }
    }
}
