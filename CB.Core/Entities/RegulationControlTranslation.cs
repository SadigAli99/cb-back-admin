

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class RegulationControlTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int RegulationControlId { get; set; }
        public RegulationControl? RegulationControl { get; set; }
        public int LanguageId { get; set; }
        public Language? Language { get; set; }
    }
}
