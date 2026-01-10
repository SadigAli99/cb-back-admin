
using CB.Core.Enums;
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.OutOfCirculationCategory
{
    public class OutOfCirculationCategoryEditDTO
    {
        public int Id { get; set; }
        public IFormFile? File { get; set; }
        public MoneyType Type { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
