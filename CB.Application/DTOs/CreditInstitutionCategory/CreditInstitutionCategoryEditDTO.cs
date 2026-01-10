

namespace CB.Application.DTOs.CreditInstitutionCategory
{
    public class CreditInstitutionCategoryEditDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
