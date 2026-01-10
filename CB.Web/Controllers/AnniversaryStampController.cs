using CB.Application.DTOs.AnniversaryStamp;
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
    public class AnniversaryStampController : ControllerBase
    {
        private readonly IAnniversaryStampService _anniversaryStampService;
        private readonly IValidator<AnniversaryStampPostDTO> _validator;

        public AnniversaryStampController(
            IAnniversaryStampService anniversaryStampService,
             IValidator<AnniversaryStampPostDTO> validator
              )
        {
            _anniversaryStampService = anniversaryStampService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _anniversaryStampService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromForm] AnniversaryStampPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _anniversaryStampService.CreateOrUpdate(dto);

            Log.Information("Yubiley möhürü məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
