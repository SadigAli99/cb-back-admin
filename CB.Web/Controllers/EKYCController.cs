using CB.Application.DTOs.EKYC;
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
    public class EKYCController : ControllerBase
    {
        private readonly IEKYCService _eKYCService;
        private readonly IValidator<EKYCPostDTO> _validator;

        public EKYCController(
            IEKYCService eKYCService,
             IValidator<EKYCPostDTO> validator
              )
        {
            _eKYCService = eKYCService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _eKYCService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] EKYCPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _eKYCService.CreateOrUpdate(dto);

            Log.Information("EKYC məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
