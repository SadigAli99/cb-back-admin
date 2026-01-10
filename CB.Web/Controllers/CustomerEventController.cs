using CB.Application.DTOs.CustomerEvent;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.CustomerEvent;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CustomerEventController : ControllerBase
    {
        private readonly ICustomerEventService _customerEventService;
        private readonly CustomerEventCreateValidator _createValidator;
        private readonly CustomerEventEditValidator _editValidator;

        public CustomerEventController(
            ICustomerEventService customerEventService,
            CustomerEventCreateValidator createValidator,
            CustomerEventEditValidator editValidator
        )
        {
            _customerEventService = customerEventService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<CustomerEventGetDTO> data = await _customerEventService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            CustomerEventGetDTO? data = await _customerEventService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("İstehlakçı tədbir məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CustomerEventCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _customerEventService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("İstehlakçı tədbir məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("İstehlakçı tədbir məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] CustomerEventEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _customerEventService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("İstehlakçı tədbir məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("İstehlakçı tədbir məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _customerEventService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("İstehlakçı tədbir məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("İstehlakçı tədbir məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

        [HttpDelete("{customerEventId}/images/{imageId}")]
        public async Task<IActionResult> DeleteImage(int customerEventId, int imageId)
        {
            var result = await _customerEventService.DeleteImageAsync(customerEventId, imageId);
            if (!result)
            {
                Log.Warning("İstehlakçı tədbir şəkli silinə bilmədi : customerEventId = {@customerEventId}, ImageId = {@ImageId}", customerEventId, imageId);
                return NotFound(new { Message = "Şəkil tapılmadı və ya silinə bilmədi." });
            }

            Log.Warning("İstehlakçı tədbir şəkli uğurla silindi : customerEventId = {@customerEventId}, ImageId = {@ImageId}", customerEventId, imageId);
            return Ok(new { Message = "Şəkil uğurla silindi." });
        }

    }
}
