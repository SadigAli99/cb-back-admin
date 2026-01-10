
using System.ComponentModel.DataAnnotations;

namespace CB.Application.DTOs.Phone
{
    public class PhoneCreateDTO
    {
        public string ContactNumber { get; set; } = null!;
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
