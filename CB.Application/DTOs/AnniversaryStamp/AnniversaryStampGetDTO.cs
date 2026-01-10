
namespace CB.Application.DTOs.AnniversaryStamp
{
    public class AnniversaryStampGetDTO
    {
        public string? Image { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
