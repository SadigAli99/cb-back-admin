using CB.Application.DTOs.VirtualEducation;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.VirtualEducation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class VirtualEducationController : ControllerBase
    {
        private readonly IVirtualEducationService _virtualEducationService;
        private readonly VirtualEducationCreateValidator _createValidator;
        private readonly VirtualEducationEditValidator _editValidator;

        public VirtualEducationController(
            IVirtualEducationService virtualEducationService,
            VirtualEducationCreateValidator createValidator,
            VirtualEducationEditValidator editValidator
        )
        {
            _virtualEducationService = virtualEducationService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<VirtualEducationGetDTO> data = await _virtualEducationService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            VirtualEducationGetDTO? data = await _virtualEducationService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Virtual təhsil məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] VirtualEducationCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _virtualEducationService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Virtual təhsil məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Virtual təhsil məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] VirtualEducationEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _virtualEducationService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Virtual təhsil məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Virtual təhsil məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _virtualEducationService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Virtual təhsil məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Virtual təhsil məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

        [HttpDelete("{virtualEducationId}/images/{imageId}")]
        public async Task<IActionResult> DeleteImage(int virtualEducationId, int imageId)
        {
            var result = await _virtualEducationService.DeleteImageAsync(virtualEducationId, imageId);
            if (!result)
            {
                Log.Warning("Virtual təhsil şəkli silinə bilmədi : VirtualEducationId = {@VirtualEducationId}, ImageId = {@ImageId}", virtualEducationId, imageId);
                return NotFound(new { Message = "Şəkil tapılmadı və ya silinə bilmədi." });
            }

            Log.Warning("Virtual təhsil şəkli uğurla silindi : VirtualEducationId = {@VirtualEducationId}, ImageId = {@ImageId}", virtualEducationId, imageId);
            return Ok(new { Message = "Şəkil uğurla silindi." });
        }

    }
}
