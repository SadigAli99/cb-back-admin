

using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.Logo
{
    public class LogoPostDTO
    {
        public IFormFile? HeaderLogo { get; set; }
        public IFormFile? FooterLogo { get; set; }
        public IFormFile? Favicon { get; set; }

    }

}
