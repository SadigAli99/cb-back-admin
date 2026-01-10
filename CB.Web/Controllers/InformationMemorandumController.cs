using CB.Application.DTOs.InformationMemorandum;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.InformationMemorandum;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InformationMemorandumController : ControllerBase
    {
        private readonly IInformationMemorandumService _informationMemorandumService;
        private readonly InformationMemorandumCreateValidator _createValidator;
        private readonly InformationMemorandumEditValidator _editValidator;

        public InformationMemorandumController(
            IInformationMemorandumService informationMemorandumService,
            InformationMemorandumCreateValidator createValidator,
            InformationMemorandumEditValidator editValidator
        )
        {
            _informationMemorandumService = informationMemorandumService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<InformationMemorandumGetDTO> data = await _informationMemorandumService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            InformationMemorandumGetDTO? data = await _informationMemorandumService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("İnformasiya memorandumu tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] InformationMemorandumCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _informationMemorandumService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("İnformasiya memorandumu əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("İnformasiya memorandumu uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] InformationMemorandumEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _informationMemorandumService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("İnformasiya memorandumu yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("İnformasiya memorandumu yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _informationMemorandumService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("İnformasiya memorandumunın silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("İnformasiya memorandumu silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
