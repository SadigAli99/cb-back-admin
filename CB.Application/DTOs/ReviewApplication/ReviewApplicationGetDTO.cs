
namespace CB.Application.DTOs.ReviewApplication
{
    public class ReviewApplicationGetDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string,string> Descriptions { get; set; } = new();
    }
}
