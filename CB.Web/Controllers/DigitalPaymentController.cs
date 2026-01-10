using CB.Application.DTOs.DigitalPayment;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.DigitalPayment;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DigitalPaymentController : ControllerBase
    {
        private readonly IDigitalPaymentService _digitalPaymentService;
        private readonly DigitalPaymentCreateValidator _createValidator;
        private readonly DigitalPaymentEditValidator _editValidator;

        public DigitalPaymentController(
            IDigitalPaymentService digitalPaymentService,
            DigitalPaymentCreateValidator createValidator,
            DigitalPaymentEditValidator editValidator
        )
        {
            _digitalPaymentService = digitalPaymentService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<DigitalPaymentGetDTO> data = await _digitalPaymentService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            DigitalPaymentGetDTO? data = await _digitalPaymentService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Rəqəmsal ödəniş məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] DigitalPaymentCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _digitalPaymentService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Rəqəmsal ödəniş məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Rəqəmsal ödəniş məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] DigitalPaymentEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _digitalPaymentService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Rəqəmsal ödəniş məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Rəqəmsal ödəniş məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _digitalPaymentService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Rəqəmsal ödəniş məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Rəqəmsal ödəniş məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
