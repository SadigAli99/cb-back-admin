using CB.Application.DTOs.CustomerProposal;
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
    public class CustomerProposalController : ControllerBase
    {
        private readonly ICustomerProposalService _customerProposalService;
        private readonly IValidator<CustomerProposalPostDTO> _validator;

        public CustomerProposalController(
            ICustomerProposalService customerProposalService,
             IValidator<CustomerProposalPostDTO> validator
              )
        {
            _customerProposalService = customerProposalService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _customerProposalService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] CustomerProposalPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { Status = "error", Messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _customerProposalService.CreateOrUpdate(dto);

            Log.Information("Müştəri təklifi məlumatı uğurla yeniləndi");

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
