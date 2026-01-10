

namespace CB.Application.DTOs.RegistrationSecurity
{
    public class RegistrationSecurityCreateDTO
    {
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
