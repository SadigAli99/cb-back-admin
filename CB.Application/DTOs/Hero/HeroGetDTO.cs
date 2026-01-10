
namespace CB.Application.DTOs.Hero
{
    public class HeroGetDTO
    {
        public string? Image { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
