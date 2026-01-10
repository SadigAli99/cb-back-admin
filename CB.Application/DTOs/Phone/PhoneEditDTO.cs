
using System.ComponentModel.DataAnnotations;

namespace CB.Application.DTOs.Phone
{
    public class PhoneEditDTO
    {
        public int Id { get; set; }
        public string ContactNumber { get; set; } = null!;
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
