
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.MoneySignProtectionElement
{
    public class MoneySignProtectionElementEditDTO
    {
        public int Id { get; set; }
        public int MoneySignHistoryId { get; set; }
        public List<IFormFile> ImageFiles { get; set; } = new();
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Values { get; set; } = new();
    }
}
