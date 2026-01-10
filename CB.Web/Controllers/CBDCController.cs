using CB.Application.DTOs.CBDC;
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
    public class CBDCController : ControllerBase
    {
        private readonly ICBDCService _cBDCService;
        private readonly IValidator<CBDCPostDTO> _validator;

        public CBDCController(
            ICBDCService cBDCService,
             IValidator<CBDCPostDTO> validator
              )
        {
            _cBDCService = cBDCService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _cBDCService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] CBDCPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _cBDCService.CreateOrUpdate(dto);

            Log.Information("MBRV məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
