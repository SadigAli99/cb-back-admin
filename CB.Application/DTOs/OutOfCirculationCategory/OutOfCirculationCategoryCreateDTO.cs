
using CB.Core.Enums;
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.OutOfCirculationCategory
{
    public class OutOfCirculationCategoryCreateDTO
    {
        public IFormFile File { get; set; } = null!;
        public MoneyType Type { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
