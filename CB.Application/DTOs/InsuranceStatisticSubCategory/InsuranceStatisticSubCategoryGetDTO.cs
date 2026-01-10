


namespace CB.Application.DTOs.InsuranceStatisticSubCategory
{
    public class InsuranceStatisticSubCategoryGetDTO
    {
        public int Id { get; set; }
        public string? InsuranceStatisticCategory { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
