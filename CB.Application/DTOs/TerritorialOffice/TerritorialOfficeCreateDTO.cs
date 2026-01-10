

namespace CB.Application.DTOs.TerritorialOffice
{
    public class TerritorialOfficeCreateDTO
    {
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
