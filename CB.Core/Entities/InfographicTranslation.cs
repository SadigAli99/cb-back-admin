

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InfographicTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int InfographicId { get; set; }
        public Infographic? Infographic { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = new();
    }
}
