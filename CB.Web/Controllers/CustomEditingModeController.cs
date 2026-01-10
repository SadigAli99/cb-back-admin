using CB.Application.DTOs.CustomEditingMode;
using CB.Application.Interfaces.Services;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CB.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomEditingModeController : ControllerBase
    {
        private readonly ICustomEditingModeService _customEditingModeService;
        private readonly IValidator<CustomEditingModePostDTO> _validator;

        public CustomEditingModeController(
            ICustomEditingModeService customEditingModeService,
             IValidator<CustomEditingModePostDTO> validator
              )
        {
            _customEditingModeService = customEditingModeService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _customEditingModeService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] CustomEditingModePostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _customEditingModeService.CreateOrUpdate(dto);

            Log.Information("Xüsusi tənzimləmə rejimi məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
