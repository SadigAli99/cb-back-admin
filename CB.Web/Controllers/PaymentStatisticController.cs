using CB.Application.DTOs.PaymentStatistic;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.PaymentStatistic;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaymentStatisticController : ControllerBase
    {
        private readonly IPaymentStatisticService _paymentStatisticService;
        private readonly PaymentStatisticCreateValidator _createValidator;
        private readonly PaymentStatisticEditValidator _editValidator;

        public PaymentStatisticController(
            IPaymentStatisticService paymentStatisticService,
            PaymentStatisticCreateValidator createValidator,
            PaymentStatisticEditValidator editValidator
        )
        {
            _paymentStatisticService = paymentStatisticService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<PaymentStatisticGetDTO> data = await _paymentStatisticService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            PaymentStatisticGetDTO? data = await _paymentStatisticService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Ödəniş statistikası məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] PaymentStatisticCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _paymentStatisticService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Ödəniş statistikası məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Ödəniş statistikası məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] PaymentStatisticEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _paymentStatisticService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Ödəniş statistikası məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Ödəniş statistikası məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _paymentStatisticService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Ödəniş statistikası məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Ödəniş statistikası məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
