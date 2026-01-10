
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.MoneySignProtection
{
    public class MoneySignProtectionCreateDTO
    {
        public int MoneySignHistoryId { get; set; }
        public IFormFile File { get; set; } = null!;
    }
}
