

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class LegalBasisTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int LegalBasisId { get; set; }
        public LegalBasis? LegalBasis { get; set; }
        public int LanguageId { get; set; }
        public Language? Language { get; set; }
    }
}
