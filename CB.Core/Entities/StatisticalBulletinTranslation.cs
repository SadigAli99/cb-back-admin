

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class StatisticalBulletinTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int StatisticalBulletinId { get; set; }
        public StatisticalBulletin? StatisticalBulletin { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = new();
    }
}
