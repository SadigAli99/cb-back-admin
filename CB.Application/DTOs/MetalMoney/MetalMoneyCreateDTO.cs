
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.MetalMoney
{
    public class MetalMoneyCreateDTO
    {
        public IFormFile File { get; set; } = null!;
    }
}
