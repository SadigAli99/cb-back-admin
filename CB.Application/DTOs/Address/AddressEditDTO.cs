
using System.ComponentModel.DataAnnotations;

namespace CB.Application.DTOs.Address
{
    public class AddressEditDTO
    {
        public int Id { get; set; }
        public bool IsMain { get; set; }
        [StringLength(10000)]
        public string? Map { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Texts { get; set; } = new();
    }

}
