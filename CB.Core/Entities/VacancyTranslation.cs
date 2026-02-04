
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class VacancyTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? Slug { get; set; }
        [StringLength(500)]
        public string? Location { get; set; }
        public int VacancyId { get; set; }
        public Vacancy? Vacancy { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = new();
    }
}
