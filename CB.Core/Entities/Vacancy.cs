
using CB.Core.Enums;

namespace CB.Core.Entities
{
    public class Vacancy : BaseEntity
    {
        public DateTime Date { get; set; }
        public VacancyType? Type { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; } = null!;
        public int BranchId { get; set; }
        public Branch Branch { get; set; } = null!;
        public int PositionId { get; set; }
        public Position Position { get; set; } = null!;
        public List<VacancyTranslation>Translations {get; set; } = new();
        public List<VacancyDetail>? Details { get; set; }
    }
}
