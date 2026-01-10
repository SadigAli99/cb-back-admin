
namespace CB.Application.DTOs.NakhchivanBlog
{
    public class NakhchivanBlogGetDTO
    {
        public int Id { get; set; }
        public List<NakhchivanBlogImageDTO> Images { get; set; } = new();
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
