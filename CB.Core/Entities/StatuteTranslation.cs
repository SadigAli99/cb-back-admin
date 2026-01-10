
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class StatuteTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? SubTitle { get; set; }
        public int StatuteId { get; set; }
        public Statute Statute { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
