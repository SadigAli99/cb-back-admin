using CB.Application.DTOs.OperatorBank;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.OperatorBank;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OperatorBankController : ControllerBase
    {
        private readonly IOperatorBankService _operatorBankService;
        private readonly OperatorBankCreateValidator _createValidator;
        private readonly OperatorBankEditValidator _editValidator;

        public OperatorBankController(
            IOperatorBankService operatorBankService,
            OperatorBankCreateValidator createValidator,
            OperatorBankEditValidator editValidator
        )
        {
            _operatorBankService = operatorBankService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<OperatorBankGetDTO> data = await _operatorBankService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            OperatorBankGetDTO? data = await _operatorBankService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Operator bank məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OperatorBankCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _operatorBankService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Operator bank məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Operator bank məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] OperatorBankEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _operatorBankService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Operator bank məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Operator bank məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _operatorBankService.DeleteAsync(id);

            if (!deleted)
            {
                Log.Warning("Operator bank məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Operator bank məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });


        }

    }
}
