

namespace CB.Application.DTOs.CreditInstitutionSubCategory
{
    public class CreditInstitutionSubCategoryCreateDTO
    {
        public int CreditInstitutionCategoryId { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
