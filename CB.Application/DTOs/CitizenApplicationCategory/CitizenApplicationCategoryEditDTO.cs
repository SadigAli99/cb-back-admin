

namespace CB.Application.DTOs.CitizenApplicationCategory
{
    public class CitizenApplicationCategoryEditDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
