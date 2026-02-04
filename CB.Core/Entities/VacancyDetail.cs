
namespace CB.Core.Entities
{
    public class VacancyDetail : BaseEntity
    {
        public int VacancyId { get; set; }
        public Vacancy? Vacancy { get; set; }
        public List<VacancyDetailTranslation>Translations {get; set; } = new();
    }
}
