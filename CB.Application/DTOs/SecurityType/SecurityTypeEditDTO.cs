

namespace CB.Application.DTOs.SecurityType
{
    public class SecurityTypeEditDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
