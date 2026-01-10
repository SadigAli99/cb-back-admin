using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.VirtualEducationCaption
{
    public class VirtualEducationCaptionPostDTO
    {
        public IFormFile? File { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
