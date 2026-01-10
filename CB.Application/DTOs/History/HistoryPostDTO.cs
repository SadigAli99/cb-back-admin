

namespace CB.Application.DTOs.History
{
    public class HistoryPostDTO
    {
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }

}
