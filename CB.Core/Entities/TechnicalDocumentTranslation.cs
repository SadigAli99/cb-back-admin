

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class TechnicalDocumentTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int TechnicalDocumentId { get; set; }
        public TechnicalDocument? TechnicalDocument { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = new();
    }
}
