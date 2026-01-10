

namespace CB.Application.DTOs.ComplaintIndexCategory
{
    public class ComplaintIndexCategoryEditDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
