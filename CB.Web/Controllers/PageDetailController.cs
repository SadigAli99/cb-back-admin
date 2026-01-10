using CB.Application.DTOs.PageDetail;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.PageDetail;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PageDetailController : ControllerBase
    {
        private readonly IPageDetailService _pageDetailService;
        private readonly PageDetailCreateValidator _createValidator;
        private readonly PageDetailEditValidator _editValidator;

        public PageDetailController(
            IPageDetailService pageDetailService,
            PageDetailCreateValidator createValidator,
            PageDetailEditValidator editValidator
        )
        {
            _pageDetailService = pageDetailService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<PageDetailGetDTO> data = await _pageDetailService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            PageDetailGetDTO? data = await _pageDetailService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Daxili səhifə məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] PageDetailCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _pageDetailService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Daxili səhifə məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Daxili səhifə məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] PageDetailEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _pageDetailService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Daxili səhifə məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Daxili səhifə məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _pageDetailService.DeleteAsync(id);

            if (!deleted)
            {
                Log.Warning("Daxili səhifə məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Daxili səhifə məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });


        }

    }
}
