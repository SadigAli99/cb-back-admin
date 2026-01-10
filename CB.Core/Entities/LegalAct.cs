
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class LegalAct : BaseEntity
    {
        [StringLength(100)]
        public string? File { get; set; } = null!;
        [StringLength(50)]
        public string? FileType { get; set; } = null!;
        public List<LegalActTranslation> Translations { get; set; } = new();
    }
}
