
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class Attestation : BaseEntity
    {
        public List<AttestationTranslation> Translations { get; set; } = new();
    }
}
