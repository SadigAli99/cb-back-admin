using CB.Application.DTOs.VirtualActive;
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
    public class VirtualActiveController : ControllerBase
    {
        private readonly IVirtualActiveService _virtualActiveService;
        private readonly IValidator<VirtualActivePostDTO> _validator;

        public VirtualActiveController(
            IVirtualActiveService virtualActiveService,
             IValidator<VirtualActivePostDTO> validator
              )
        {
            _virtualActiveService = virtualActiveService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _virtualActiveService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] VirtualActivePostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _virtualActiveService.CreateOrUpdate(dto);

            Log.Information("Virtual aktiv məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
