

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class AttestationTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(50000)]
        public string? Description { get; set; }
        public int AttestationId { get; set; }
        public Attestation? Attestation { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
