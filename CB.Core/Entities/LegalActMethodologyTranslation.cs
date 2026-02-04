

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class LegalActMethodologyTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int LegalActMethodologyId { get; set; }
        public LegalActMethodology? LegalActMethodology { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = new();
    }
}
