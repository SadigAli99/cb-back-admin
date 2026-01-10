

namespace CB.Application.DTOs.ParticipantCategory
{
    public class ParticipantCategoryEditDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
