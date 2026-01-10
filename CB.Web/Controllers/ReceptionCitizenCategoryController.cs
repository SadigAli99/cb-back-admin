using CB.Application.DTOs.ReceptionCitizenCategory;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.ReceptionCitizenCategory;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReceptionCitizenCategoryController : ControllerBase
    {
        private readonly IReceptionCitizenCategoryService _receptionCitizenCategoryService;
        private readonly ReceptionCitizenCategoryCreateValidator _createValidator;
        private readonly ReceptionCitizenCategoryEditValidator _editValidator;

        public ReceptionCitizenCategoryController(
            IReceptionCitizenCategoryService receptionCitizenCategoryService,
            ReceptionCitizenCategoryCreateValidator createValidator,
            ReceptionCitizenCategoryEditValidator editValidator
        )
        {
            _receptionCitizenCategoryService = receptionCitizenCategoryService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /api/receptionCitizenCategory
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _receptionCitizenCategoryService.GetAllAsync();
            return Ok(data);
        }

        // GET: /api/receptionCitizenCategory/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _receptionCitizenCategoryService.GetByIdAsync(id);
            if (data == null)
            {
                Log.Warning("Vətəndaşların qeydiyyatı kateqoriya məlumatı tapılmadı. Id={Id}", id);
                return NotFound();
            }

            return Ok(data);
        }

        // POST: /api/receptionCitizenCategory
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReceptionCitizenCategoryCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });
            var created = await _receptionCitizenCategoryService.CreateAsync(dto);

            if (!created)
            {
                Log.Error("Vətəndaşların qeydiyyatı kateqoriya əlavə edilməsi uğursuz oldu {@Dto}", dto);
                return BadRequest(new { status = "error", message = "Məlumat əlavə olunmadı" });
            }

            Log.Information("Məlumat uğurla əlavə olundu {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // PUT: /api/receptionCitizenCategory/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] ReceptionCitizenCategoryEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(e => e.ErrorMessage) });

            var updated = await _receptionCitizenCategoryService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Error("Vətəndaşların qeydiyyatı kateqoriya yenilənməsi uğursuz oldu {@Dto}", dto);
                return NotFound();
            }

            Log.Information("Vətəndaşların qeydiyyatı kateqoriya yenilənməsi uğurludur {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }

        // DELETE: /api/receptionCitizenCategory/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _receptionCitizenCategoryService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Vətəndaşların qeydiyyatı kateqoriya silinməsi uğursuz oldu Id={@Id}", id);
                return NotFound();
            }
            Log.Information("Vətəndaşların qeydiyyatı kateqoriya uğurla silindi : Id={@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });
        }
    }
}
