
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class AttestationFile : BaseEntity
    {
        [StringLength(100)]
        public string? File { get; set; } = null!;
        [StringLength(50)]
        public string? FileType { get; set; } = null!;
        public int AttestationId { get; set; }
        public Attestation Attestation { get; set; } = null!;
        public List<AttestationFileTranslation> Translations { get; set; } = new();
    }
}
