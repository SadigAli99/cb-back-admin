

namespace CB.Application.DTOs.CreditInstitutionSubCategory
{
    public class CreditInstitutionSubCategoryEditDTO
    {
        public int Id { get; set; }
        public int CreditInstitutionCategoryId { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
