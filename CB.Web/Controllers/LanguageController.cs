using CB.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CB.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LanguageController : ControllerBase
    {
        private readonly ILanguageService _languageService;
        public LanguageController(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<string> languages = await _languageService.GetLanguageCodes();
            return Ok(languages);
        }
    }
}
