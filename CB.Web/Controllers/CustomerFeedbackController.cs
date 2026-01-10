using CB.Application.DTOs.CustomerFeedback;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.CustomerFeedback;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CustomerFeedbackController : ControllerBase
    {
        private readonly ICustomerFeedbackService _customerFeedbackService;
        private readonly CustomerFeedbackCreateValidator _createValidator;
        private readonly CustomerFeedbackEditValidator _editValidator;

        public CustomerFeedbackController(
            ICustomerFeedbackService customerFeedbackService,
            CustomerFeedbackCreateValidator createValidator,
            CustomerFeedbackEditValidator editValidator
        )
        {
            _customerFeedbackService = customerFeedbackService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<CustomerFeedbackGetDTO> data = await _customerFeedbackService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            CustomerFeedbackGetDTO? data = await _customerFeedbackService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Müştəri rəyləri məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerFeedbackCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _customerFeedbackService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Müştəri rəyləri məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Müştəri rəyləri məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] CustomerFeedbackEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _customerFeedbackService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Müştəri rəyləri məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Müştəri rəyləri məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _customerFeedbackService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Müştəri rəyləri məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Müştəri rəyləri məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
