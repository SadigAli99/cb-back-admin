


namespace CB.Application.DTOs.CreditInstitutionSubCategory
{
    public class CreditInstitutionSubCategoryGetDTO
    {
        public int Id { get; set; }
        public string? CreditInstitutionCategory { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
