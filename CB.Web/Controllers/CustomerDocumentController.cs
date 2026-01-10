using CB.Application.DTOs.CustomerDocument;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.CustomerDocument;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CustomerDocumentController : ControllerBase
    {
        private readonly ICustomerDocumentService _customerDocumentService;
        private readonly CustomerDocumentCreateValidator _createValidator;
        private readonly CustomerDocumentEditValidator _editValidator;

        public CustomerDocumentController(
            ICustomerDocumentService customerDocumentService,
            CustomerDocumentCreateValidator createValidator,
            CustomerDocumentEditValidator editValidator
        )
        {
            _customerDocumentService = customerDocumentService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<CustomerDocumentGetDTO> data = await _customerDocumentService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            CustomerDocumentGetDTO? data = await _customerDocumentService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("İstehlakçı sənəd məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerDocumentCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _customerDocumentService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("İstehlakçı sənəd məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("İstehlakçı sənəd məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] CustomerDocumentEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _customerDocumentService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("İstehlakçı sənəd məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("İstehlakçı sənəd məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _customerDocumentService.DeleteAsync(id);

            if (!deleted)
            {
                Log.Warning("İstehlakçı sənəd məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("İstehlakçı sənəd məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });


        }

    }
}
