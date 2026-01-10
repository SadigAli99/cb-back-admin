

using CB.Core.Enums;
using Microsoft.AspNetCore.Http;

namespace CB.Application.DTOs.Vacancy
{
    public class VacancyCreateDTO
    {
        public DateTime Date { get; set; }
        public int DepartmentId { get; set; }
        public int BranchId { get; set; }
        public int PositionId { get; set; }
        public VacancyType Type { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Slugs { get; set; } = new();
        public Dictionary<string, string> Locations { get; set; } = new();
    }
}
