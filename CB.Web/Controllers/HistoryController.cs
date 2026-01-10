using CB.Application.DTOs.History;
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
    public class HistoryController : ControllerBase
    {
        private readonly IHistoryService _historyService;
        private readonly IValidator<HistoryPostDTO> _validator;

        public HistoryController(
            IHistoryService historyService,
             IValidator<HistoryPostDTO> validator
              )
        {
            _historyService = historyService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _historyService.GetFirst();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] HistoryPostDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { status = "error", messages = validationResult.Errors.Select(x => x.ErrorMessage) });


            await _historyService.CreateOrUpdate(dto);
            Log.Information("Tarixçə məlumatının yenilənməsi uğurludur : {@Dto}", dto);

            return Ok(new { status = "success", message = "Məlumat uğurla yeniləndi" });
        }
    }
}
