
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class OtherLaw : BaseEntity
    {
        [StringLength(100)]
        public string? File { get; set; } = null!;
        [StringLength(50)]
        public string? FileType { get; set; } = null!;
        [StringLength(500)]
        public string? Url { get; set; }
        public List<OtherLawTranslation> Translations { get; set; } = new();
    }
}
