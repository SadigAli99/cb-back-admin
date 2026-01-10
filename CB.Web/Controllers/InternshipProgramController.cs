using CB.Application.DTOs.InternshipProgram;
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
    public class InternshipProgramController : ControllerBase
    {
        private readonly IInternshipProgramService _internshipProgramService;
        private readonly IValidator<InternshipProgramPostDTO> _validator;

        public InternshipProgramController(
            IInternshipProgramService internshipProgramService,
             IValidator<InternshipProgramPostDTO> validator
              )
        {
            _internshipProgramService = internshipProgramService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _internshipProgramService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] InternshipProgramPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _internshipProgramService.CreateOrUpdate(dto);

            Log.Information("Təcrübə proqramı məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
