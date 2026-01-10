

using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.StructureCaption
{
    public class StructureCaptionPostDTO
    {
        public IFormFile? File { get; set; }
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }

}
