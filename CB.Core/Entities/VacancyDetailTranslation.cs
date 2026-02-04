
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class VacancyDetailTranslation : BaseEntity
    {
        [StringLength(200)]
        public string? Title { get; set; }
        [StringLength(50000)]
        public string? Description { get; set; }
        public int VacancyDetailId { get; set; }
        public VacancyDetail? VacancyDetail { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = new();
    }
}
