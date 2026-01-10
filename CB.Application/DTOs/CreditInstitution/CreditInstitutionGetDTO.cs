
namespace CB.Application.DTOs.CreditInstitution
{
    public class CreditInstitutionGetDTO
    {
        public int Id { get; set; }
        public string? CreditInstitutionCategory { get; set; }
        public string? CreditInstitutionSubCategory { get; set; }
        public string? File { get; set; }
        public string? FileType { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();

    }
}
