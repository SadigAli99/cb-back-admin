
using CB.Core.Enums;


namespace CB.Application.DTOs.Vacancy
{
    public class VacancyGetDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public VacancyType Type { get; set; }
        public string? DepartmentTitle { get; set; }
        public string? BranchTitle { get; set; }
        public string? PositionTitle { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Locations { get; set; } = new();
    }
}
