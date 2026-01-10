

namespace CB.Application.DTOs.ReceptionCitizen
{
    public class ReceptionCitizenEditDTO
    {
        public int Id { get; set; }
        public int ReceptionCitizenCategoryId { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
