
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.StateProgram
{
    public class StateProgramCreateDTO
    {
        public IFormFile File { get; set; } = null!;
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> CoverTitles { get; set; } = new();
        public int StateProgramCategoryId { get; set; }
    }
}
