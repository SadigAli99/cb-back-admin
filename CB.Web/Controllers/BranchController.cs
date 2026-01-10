using CB.Application.DTOs.Branch;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.Branch;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _branchService;
        private readonly BranchCreateValidator _createValidator;
        private readonly BranchEditValidator _editValidator;

        public BranchController(
            IBranchService branchService,
            BranchCreateValidator createValidator,
            BranchEditValidator editValidator
        )
        {
            _branchService = branchService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/branch
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _branchService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/branch/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _branchService.GetByIdAsync(id);
            if (data == null)
            {
                Log.Warning("Şöbə məlumatı tapılmadı. Id={Id}", id);
                return NotFound();
            }

            return Ok(data);
        }

        // POST: /api/branch
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BranchCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _branchService.CreateAsync(dto);

            if (!created)
            {
                Log.Error("Şöbə əlavə edilməsi uğursuz oldu {@Dto}", dto);
                return BadRequest(new { status = "error", message = "Məlumat əlavə olunmadı" });
            }

            Log.Information("Məlumat uğurla əlavə olundu {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/branch/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] BranchEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });

            var updated = await _branchService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Error("Şöbə yenilənməsi uğursuz oldu {@Dto}", dto);
                return NotFound();
            }

            Log.Information("Şöbə yenilənməsi uğurludur {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        // DELETE: /api/branch/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _branchService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Şöbə silinməsi uğursuz oldu Id={@Id}", id);
                return NotFound();
            }
            Log.Information("Şöbə uğurla silindi : Id={@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }
    }
}
