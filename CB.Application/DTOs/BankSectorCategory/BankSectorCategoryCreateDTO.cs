

namespace CB.Application.DTOs.BankSectorCategory
{
    public class BankSectorCategoryCreateDTO
    {
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Slugs { get; set; } = new();
        public Dictionary<string, string> Notes { get; set; } = new();
    }

}
