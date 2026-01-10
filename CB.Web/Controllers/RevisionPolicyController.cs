using CB.Application.DTOs.RevisionPolicy;
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
    public class RevisionPolicyController : ControllerBase
    {
        private readonly IRevisionPolicyService _revisionPolicyService;
        private readonly IValidator<RevisionPolicyPostDTO> _validator;

        public RevisionPolicyController(
            IRevisionPolicyService revisionPolicyService,
             IValidator<RevisionPolicyPostDTO> validator
              )
        {
            _revisionPolicyService = revisionPolicyService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _revisionPolicyService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] RevisionPolicyPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _revisionPolicyService.CreateOrUpdate(dto);

            Log.Information("Reviziya siyasəti məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
