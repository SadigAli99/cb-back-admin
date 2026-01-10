

namespace CB.Application.DTOs.InsuranceStatisticCategory
{
    public class InsuranceStatisticCategoryEditDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
