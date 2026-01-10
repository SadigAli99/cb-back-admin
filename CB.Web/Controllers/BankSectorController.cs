using CB.Application.DTOs.BankSector;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.BankSector;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class BankSectorController : ControllerBase
    {
        private readonly IBankSectorService _bankSectorService;
        private readonly BankSectorCreateValidator _createValidator;
        private readonly BankSectorEditValidator _editValidator;

        public BankSectorController(
            IBankSectorService bankSectorService,
            BankSectorCreateValidator createValidator,
            BankSectorEditValidator editValidator
            )
        {
            _bankSectorService = bankSectorService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        // GET: /BankSector
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _bankSectorService.GetAllAsync();
            return Ok(data);
        }

        // POST: /BankSector
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BankSectorCreateDTO dto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool created = await _bankSectorService.CreateAsync(dto);
            if (!created)
            {
                Log.Warning("Bank sektor məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dto);
                return BadRequest();
            }

            Log.Information("Bank sektor məlumatı uğurla əlavə olundu : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        // GET: /BankSector/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _bankSectorService.GetForEditAsync(id);
            if (data is null)
            {
                Log.Warning("Bank sektor məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        // PUT : /BankSector/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, BankSectorEditDTO dto)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            var updated = await _bankSectorService.UpdateAsync(id, dto);
            if (!updated)
            {
                Log.Warning("Bank sektor məlumatı yenilənməsi uğursuz oldu : {@Dto}", dto);
                return NotFound();
            }
            Log.Information("Bank sektor məlumatı yenilənməsi uğurludur : {@Dto}", dto);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });

        }

        // DELETE: /BankSector/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _bankSectorService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Bank sektor məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Bank sektor məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }
    }
}
