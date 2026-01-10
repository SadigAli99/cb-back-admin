using CB.Application.DTOs.MetalMoney;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.MetalMoney;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MetalMoneyController : ControllerBase
    {
        private readonly IMetalMoneyService _metalMoneyService;
        private readonly MetalMoneyCreateValidator _createValidator;
        private readonly MetalMoneyEditValidator _editValidator;

        public MetalMoneyController(
            IMetalMoneyService metalMoneyService,
            MetalMoneyCreateValidator createValidator,
            MetalMoneyEditValidator editValidator
        )
        {
            _metalMoneyService = metalMoneyService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<MetalMoneyGetDTO> data = await _metalMoneyService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            MetalMoneyGetDTO? data = await _metalMoneyService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Metal pul əskinasları məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] MetalMoneyCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _metalMoneyService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Metal pul əskinasları məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Metal pul əskinasları məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] MetalMoneyEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _metalMoneyService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Metal pul əskinasları məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Metal pul əskinasları məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _metalMoneyService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Metal pul əskinasları məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Metal pul əskinasları məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
