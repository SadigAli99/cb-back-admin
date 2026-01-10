

namespace CB.Application.DTOs.FaqCategory
{
    public class FaqCategoryEditDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
