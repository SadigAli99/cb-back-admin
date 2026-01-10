

namespace CB.Application.DTOs.Attestation
{
    public class AttestationCreateDTO
    {
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
