using CB.Application.DTOs.MonetaryPolicyVideo;
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
    public class MonetaryPolicyVideoController : ControllerBase
    {
        private readonly IMonetaryPolicyVideoService _monetaryPolicyVideoService;
        private readonly IValidator<MonetaryPolicyVideoPostDTO> _validator;

        public MonetaryPolicyVideoController(
            IMonetaryPolicyVideoService monetaryPolicyVideoService,
             IValidator<MonetaryPolicyVideoPostDTO> validator
              )
        {
            _monetaryPolicyVideoService = monetaryPolicyVideoService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _monetaryPolicyVideoService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] MonetaryPolicyVideoPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _monetaryPolicyVideoService.CreateOrUpdate(dto);

            Log.Information("Pul siyasəti video məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
