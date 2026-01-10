
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.InternationalCooperationInitiative
{
    public class InternationalCooperationInitiativeCreateDTO
    {
        public IFormFile File { get; set; } = null!;
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> CoverTitles { get; set; } = new();
    }
}
