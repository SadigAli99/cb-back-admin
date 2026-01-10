

namespace CB.Application.DTOs.TerritorialOffice
{
    public class TerritorialOfficeEditDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
