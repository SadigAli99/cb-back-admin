
namespace CB.Application.DTOs.Meas
{
    public class MeasGetDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public DateTime RegisterTime { get; set; }
        public string? RegisterNumber { get; set; }
        public string? PdfFile { get; set; }
        public string? IssuerTypeTitle { get; set; }
        public string? InformationTypeTitle { get; set; }
        public string? SecurityTypeTitle { get; set; }
    }
}
