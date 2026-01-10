
namespace CB.Application.DTOs.AnniversaryCoin
{
    public class AnniversaryCoinGetDTO
    {
        public string? Image { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }

}
