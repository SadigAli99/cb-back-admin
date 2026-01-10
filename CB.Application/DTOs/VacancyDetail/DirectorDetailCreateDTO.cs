

namespace CB.Application.DTOs.VacancyDetail
{
    public class VacancyDetailCreateDTO
    {
        public int VacancyId { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Descriptions { get; set; } = new();
    }
}
