
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.CreditInstitution
{
    public class CreditInstitutionCreateDTO
    {
        public int CreditInstitutionCategoryId { get; set; }
        public int? CreditInstitutionSubCategoryId { get; set; }
        public IFormFile File { get; set; } = null!;
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> CoverTitles { get; set; } = new();
    }
}
