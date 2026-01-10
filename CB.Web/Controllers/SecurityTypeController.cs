using CB.Application.DTOs.SecurityType;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.SecurityType;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class SecurityTypeController : ControllerBase
    {
        private readonly ISecurityTypeService _securityTypeService;
        private readonly SecurityTypeCreateValidator _createValidator;
        private readonly SecurityTypeEditValidator _editValidator;

        public SecurityTypeController(
            ISecurityTypeService securityTypeService,
            SecurityTypeCreateValidator createValidator,
            SecurityTypeEditValidator editValidator
        )
        {
            _securityTypeService = securityTypeService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/securityType
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _securityTypeService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/securityType/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _securityTypeService.GetByIdAsync(id);
            if (data == null)
            {
                Log.Warning("Qiymətli kağız növü məlumatı tapılmadı. Id={Id}", id);
                return NotFound();
            }

            return Ok(data);
        }

        // POST: /api/securityType
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SecurityTypeCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _securityTypeService.CreateAsync(dto);

            if (!created)
            {
                Log.Error("Qiymətli kağız növü əlavə edilməsi uğursuz oldu {@Dto}", dto);
                return BadRequest(new { status = "error", message = "Məlumat əlavə olunmadı" });
            }

            Log.Information("Məlumat uğurla əlavə olundu {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/securityType/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] SecurityTypeEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });

            var updated = await _securityTypeService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Error("Qiymətli kağız növü yenilənməsi uğursuz oldu {@Dto}", dto);
                return NotFound();
            }

            Log.Information("Qiymətli kağız növü yenilənməsi uğurludur {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        // DELETE: /api/securityType/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _securityTypeService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Qiymətli kağız növü silinməsi uğursuz oldu Id={@Id}", id);
                return NotFound();
            }
            Log.Information("Qiymətli kağız növü uğurla silindi : Id={@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }
    }
}
