

namespace CB.Application.DTOs.InformationType
{
    public class InformationTypeEditDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
