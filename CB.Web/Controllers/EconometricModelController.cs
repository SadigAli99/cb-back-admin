using CB.Application.DTOs.EconometricModel;
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
    public class EconometricModelController : ControllerBase
    {
        private readonly IEconometricModelService _econometricModelService;
        private readonly IValidator<EconometricModelPostDTO> _validator;

        public EconometricModelController(
            IEconometricModelService econometricModelService,
             IValidator<EconometricModelPostDTO> validator
              )
        {
            _econometricModelService = econometricModelService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _econometricModelService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] EconometricModelPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _econometricModelService.CreateOrUpdate(dto);

            Log.Information("Ekonometrik model məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
