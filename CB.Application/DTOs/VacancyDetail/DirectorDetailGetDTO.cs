
namespace CB.Application.DTOs.VacancyDetail
{
    public class VacancyDetailGetDTO
    {
        public int Id { get; set; }
        public string? VacancyName { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
