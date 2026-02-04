

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class MethodologyTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int MethodologyId { get; set; }
        public Methodology? Methodology { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = new();
    }
}
