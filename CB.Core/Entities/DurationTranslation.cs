
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class DurationTranslation : BaseEntity
    {
        [StringLength(50)]
        public string? Title { get; set; }
        public int DurationId { get; set; }
        public Duration? Duration { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = new();
    }
}
