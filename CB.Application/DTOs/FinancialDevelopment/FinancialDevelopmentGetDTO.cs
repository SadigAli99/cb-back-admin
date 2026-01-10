
namespace CB.Application.DTOs.FinancialDevelopment
{
    public class FinancialDevelopmentGetDTO
    {
        public string? Image { get; set; }
        public string? PdfFile { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
        public Dictionary<string, string> FileHeadTitles { get; set; } = new();
        public Dictionary<string, string> FileTitles { get; set; } = new();
    }

}
