
namespace CB.Application.DTOs.Attestation
{
    public class AttestationEditDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descrtiptions { get; set; } = new();
    }
}
