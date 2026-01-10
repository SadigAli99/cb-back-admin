
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.MoneySignProtection
{
    public class MoneySignProtectionEditDTO
    {
        public int Id { get; set; }
        public int MoneySignHistoryId { get; set; }
        public IFormFile? File { get; set; }
    }
}
