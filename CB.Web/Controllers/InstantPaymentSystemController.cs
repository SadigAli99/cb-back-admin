using CB.Application.DTOs.InstantPaymentSystem;
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
    public class InstantPaymentSystemControlFileler : ControllerBase
    {
        private readonly IInstantPaymentSystemService _instantPaymentSystemService;
        private readonly IValidator<InstantPaymentSystemPostDTO> _validator;

        public InstantPaymentSystemControlFileler(
            IInstantPaymentSystemService instantPaymentSystemService,
             IValidator<InstantPaymentSystemPostDTO> validator
              )
        {
            _instantPaymentSystemService = instantPaymentSystemService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _instantPaymentSystemService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] InstantPaymentSystemPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _instantPaymentSystemService.CreateOrUpdate(dto);

            Log.Information("Ani ödəmə sistemi məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
