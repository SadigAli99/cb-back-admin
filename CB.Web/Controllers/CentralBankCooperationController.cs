using CB.Application.DTOs.CentralBankCooperation;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.CentralBankCooperation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CentralBankCooperationController : ControllerBase
    {
        private readonly ICentralBankCooperationService _centralBankCooperationService;
        private readonly CentralBankCooperationCreateValidator _createValidator;
        private readonly CentralBankCooperationEditValidator _editValidator;

        public CentralBankCooperationController(
            ICentralBankCooperationService centralBankCooperationService,
            CentralBankCooperationCreateValidator createValidator,
            CentralBankCooperationEditValidator editValidator
        )
        {
            _centralBankCooperationService = centralBankCooperationService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<CentralBankCooperationGetDTO> data = await _centralBankCooperationService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            CentralBankCooperationGetDTO? data = await _centralBankCooperationService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Mərkəzi banklarla əməkdaşlıq məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CentralBankCooperationCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _centralBankCooperationService.CreateAsync(dTO);

            if (!created)
            {
                Log.Warning("Mərkəzi banklarla əməkdaşlıq məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Mərkəzi banklarla əməkdaşlıq məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] CentralBankCooperationEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _centralBankCooperationService.UpdateAsync(id, dTO);

            if (!updated)
            {
                Log.Warning("Mərkəzi banklarla əməkdaşlıq məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Mərkəzi banklarla əməkdaşlıq məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _centralBankCooperationService.DeleteAsync(id);

            if (!deleted)
            {
                Log.Warning("Mərkəzi banklarla əməkdaşlıq məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Mərkəzi banklarla əməkdaşlıq məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
