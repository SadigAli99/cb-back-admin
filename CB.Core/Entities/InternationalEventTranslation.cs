
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InternationalEventTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? Slug { get; set; }
        [StringLength(50000)]
        public string? Description { get; set; }
        public int InternationalEventId { get; set; }
        public InternationalEvent InternationalEvent { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
