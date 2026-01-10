

namespace CB.Application.DTOs.CapitalMarketCategory
{
    public class CapitalMarketCategoryEditDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
