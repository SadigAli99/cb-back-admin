

namespace CB.Application.DTOs.InsuranceStatisticSubCategory
{
    public class InsuranceStatisticSubCategoryEditDTO
    {
        public int Id { get; set; }
        public int InsuranceStatisticCategoryId { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
