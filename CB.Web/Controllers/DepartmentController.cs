using CB.Application.DTOs.Department;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.Department;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly DepartmentCreateValidator _createValidator;
        private readonly DepartmentEditValidator _editValidator;

        public DepartmentController(
            IDepartmentService departmentService,
            DepartmentCreateValidator createValidator,
            DepartmentEditValidator editValidator
        )
        {
            _departmentService = departmentService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/department
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _departmentService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/department/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _departmentService.GetByIdAsync(id);
            if (data == null)
            {
                Log.Warning("Departament məlumatı tapılmadı. Id={Id}", id);
                return NotFound();
            }

            return Ok(data);
        }

        // POST: /api/department
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DepartmentCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _departmentService.CreateAsync(dto);

            if (!created)
            {
                Log.Error("Departament əlavə edilməsi uğursuz oldu {@Dto}", dto);
                return BadRequest(new { status = "error", message = "Məlumat əlavə olunmadı" });
            }

            Log.Information("Məlumat uğurla əlavə olundu {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/department/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] DepartmentEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });

            var updated = await _departmentService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Error("Departament yenilənməsi uğursuz oldu {@Dto}", dto);
                return NotFound();
            }

            Log.Information("Departament yenilənməsi uğurludur {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        // DELETE: /api/department/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _departmentService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Departament silinməsi uğursuz oldu Id={@Id}", id);
                return NotFound();
            }
            Log.Information("Departament uğurla silindi : Id={@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }
    }
}
