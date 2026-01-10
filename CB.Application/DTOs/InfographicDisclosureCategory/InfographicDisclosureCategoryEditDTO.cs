

namespace CB.Application.DTOs.InfographicDisclosureCategory
{
    public class InfographicDisclosureCategoryEditDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
