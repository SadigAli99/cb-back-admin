

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class RegulationTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int RegulationId { get; set; }
        public Regulation? Regulation { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = new();
    }
}
