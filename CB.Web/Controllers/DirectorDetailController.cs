using CB.Application.DTOs.DirectorDetail;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.DirectorDetail;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DirectorDetailController : ControllerBase
    {
        private readonly IDirectorDetailService _directorDetailService;
        private readonly DirectorDetailCreateValidator _createValidator;
        private readonly DirectorDetailEditValidator _editValidator;

        public DirectorDetailController(
            IDirectorDetailService directorDetailService,
            DirectorDetailCreateValidator createValidator,
            DirectorDetailEditValidator editValidator
        )
        {
            _directorDetailService = directorDetailService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int id)
        {
            List<DirectorDetailGetDTO> data = await _directorDetailService.GetAllAsync(id);
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            DirectorDetailGetDTO? data = await _directorDetailService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Rəhbər məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DirectorDetailCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _directorDetailService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Rəhbər məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Rəhbər məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] DirectorDetailEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _directorDetailService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Rəhbər məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Rəhbər məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _directorDetailService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Rəhbər məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Rəhbər məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
