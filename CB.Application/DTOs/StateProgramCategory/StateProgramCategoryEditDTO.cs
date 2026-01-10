

namespace CB.Application.DTOs.StateProgramCategory
{
    public class StateProgramCategoryEditDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
