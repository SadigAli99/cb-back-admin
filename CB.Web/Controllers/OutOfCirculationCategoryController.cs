using CB.Application.DTOs.OutOfCirculationCategory;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.OutOfCirculationCategory;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OutOfCirculationCategoryController : ControllerBase
    {
        private readonly IOutOfCirculationCategoryService _outOfCirculationCategoryService;
        private readonly OutOfCirculationCategoryCreateValidator _createValidator;
        private readonly OutOfCirculationCategoryEditValidator _editValidator;

        public OutOfCirculationCategoryController(
            IOutOfCirculationCategoryService outOfCirculationCategoryService,
            OutOfCirculationCategoryCreateValidator createValidator,
            OutOfCirculationCategoryEditValidator editValidator
        )
        {
            _outOfCirculationCategoryService = outOfCirculationCategoryService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<OutOfCirculationCategoryGetDTO> data = await _outOfCirculationCategoryService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            OutOfCirculationCategoryGetDTO? data = await _outOfCirculationCategoryService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Tədavüldə olmayan pul əskinasları məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] OutOfCirculationCategoryCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _outOfCirculationCategoryService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Tədavüldə olmayan pul əskinasları məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Tədavüldə olmayan pul əskinasları məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] OutOfCirculationCategoryEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _outOfCirculationCategoryService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Tədavüldə olmayan pul əskinasları məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Tədavüldə olmayan pul əskinasları məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _outOfCirculationCategoryService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Tədavüldə olmayan pul əskinasları məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Tədavüldə olmayan pul əskinasları məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
