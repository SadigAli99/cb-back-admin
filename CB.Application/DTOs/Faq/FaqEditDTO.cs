

namespace CB.Application.DTOs.Faq
{
    public class FaqEditDTO
    {
        public int Id { get; set; }
        public int FaqCategoryId { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
