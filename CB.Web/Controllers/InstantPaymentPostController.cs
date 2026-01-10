using CB.Application.DTOs.InstantPaymentPost;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.InstantPaymentPost;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InstantPaymentPostController : ControllerBase
    {
        private readonly IInstantPaymentPostService _instantPaymentPostService;
        private readonly InstantPaymentPostCreateValidator _createValidator;
        private readonly InstantPaymentPostEditValidator _editValidator;

        public InstantPaymentPostController(
            IInstantPaymentPostService instantPaymentPostService,
            InstantPaymentPostCreateValidator createValidator,
            InstantPaymentPostEditValidator editValidator
        )
        {
            _instantPaymentPostService = instantPaymentPostService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<InstantPaymentPostGetDTO> data = await _instantPaymentPostService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            InstantPaymentPostGetDTO? data = await _instantPaymentPostService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Ani ödəmə məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] InstantPaymentPostCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _instantPaymentPostService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Ani ödəmə məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Ani ödəmə məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] InstantPaymentPostEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _instantPaymentPostService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Ani ödəmə məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Ani ödəmə məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _instantPaymentPostService.DeleteAsync(id);

            if (!deleted)
            {
                Log.Warning("Ani ödəmə məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Ani ödəmə məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });


        }

    }
}
