using CB.Application.DTOs.PostalCommunication;
using CB.Application.Interfaces.Services;
using CB.Application.Validators.PostalCommunication;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PostalCommunicationController : ControllerBase
    {
        private readonly IPostalCommunicationService _postalCommunicationService;
        private readonly PostalCommunicationCreateValidator _createValidator;
        private readonly PostalCommunicationEditValidator _editValidator;

        public PostalCommunicationController(
            IPostalCommunicationService postalCommunicationService,
            PostalCommunicationCreateValidator createValidator,
            PostalCommunicationEditValidator editValidator
        )
        {
            _postalCommunicationService = postalCommunicationService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<PostalCommunicationGetDTO> data = await _postalCommunicationService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            PostalCommunicationGetDTO? data = await _postalCommunicationService.GetByIdAsync(id);
            if (data is null)
            {
                Log.Warning("Poçt rabitəsinin milli operatoru məlumatı tapılmadı : Id = {@Id}", id);
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] PostalCommunicationCreateDTO dTO)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });
            bool created = await _postalCommunicationService.CreateAsync(dTO);
            if (!created)
            {
                Log.Warning("Poçt rabitəsinin milli operatoru məlumatı əlavə edilməsi uğursuz oldu : {@Dto}", dTO);
                return BadRequest();
            }

            Log.Information("Poçt rabitəsinin milli operatoru məlumatı uğurla əlavə olundu : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla əlavə olundu" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] PostalCommunicationEditDTO dTO)
        {
            ValidationResult validationResult = await _editValidator.ValidateAsync(dTO);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });

            bool updated = await _postalCommunicationService.UpdateAsync(id, dTO);
            if (!updated)
            {
                Log.Warning("Poçt rabitəsinin milli operatoru məlumatı yenilənməsi uğursuz oldu : {@Dto}", dTO);
                return NotFound();
            }
            Log.Information("Poçt rabitəsinin milli operatoru məlumatı yenilənməsi uğurludur : {@Dto}", dTO);
            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _postalCommunicationService.DeleteAsync(id);
            if (!deleted)
            {
                Log.Warning("Poçt rabitəsinin milli operatoru məlumatının silinməsi uğursuzdur : Id = {@Id}", id);
                return NotFound();
            }
            Log.Information("Poçt rabitəsinin milli operatoru məlumatı silinməsi uğurludur : Id = {@Id}", id);
            return Ok(new { status = "success", message = "Məlumat uğurla silindi" });

        }

    }
}
