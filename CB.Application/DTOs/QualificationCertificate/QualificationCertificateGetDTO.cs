
namespace CB.Application.DTOs.QualificationCertificate
{
    public class QualificationCertificateGetDTO
    {
        public int Id { get; set; }
        public string? File { get; set; }
        public string? FileType { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
