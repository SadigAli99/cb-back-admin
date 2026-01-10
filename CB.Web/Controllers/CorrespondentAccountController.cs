using CB.Application.DTOs.CorrespondentAccount;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.CorrespondentAccount;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CorrespondentAccountController : ControllerBase
    {
        private readonly ICorrespondentAccountService _correspondentAccountService;
        private readonly CorrespondentAccountCreateValidator _createValidator;
        private readonly CorrespondentAccountEditValidator _editValidator;

        public CorrespondentAccountController(
            ICorrespondentAccountService correspondentAccountService,
            CorrespondentAccountCreateValidator createValidator,
            CorrespondentAccountEditValidator editValidator
        )
        {
            _correspondentAccountService = correspondentAccountService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<CorrespondentAccountGetDTO> data = await _correspondentAccountService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            CorrespondentAccountGetDTO? data = await _correspondentAccountService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Müxbir hesabları məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CorrespondentAccountCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _correspondentAccountService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Müxbir hesabları məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Müxbir hesabları məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] CorrespondentAccountEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _correspondentAccountService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Müxbir hesabları məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Müxbir hesabları məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _correspondentAccountService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Müxbir hesabları məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Müxbir hesabları məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
