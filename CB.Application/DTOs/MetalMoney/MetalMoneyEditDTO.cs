
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.MetalMoney
{
    public class MetalMoneyEditDTO
    {
        public int Id { get; set; }
        public IFormFile? File { get; set; }
    }
}
