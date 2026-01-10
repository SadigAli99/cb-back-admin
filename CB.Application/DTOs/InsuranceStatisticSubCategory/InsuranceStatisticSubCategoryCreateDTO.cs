

namespace CB.Application.DTOs.InsuranceStatisticSubCategory
{
    public class InsuranceStatisticSubCategoryCreateDTO
    {
        public int InsuranceStatisticCategoryId { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
