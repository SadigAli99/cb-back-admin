

using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.CBAR105Event
{
    public class CBAR105EventPostDTO
    {
        public List<IFormFile> Files { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }

}
