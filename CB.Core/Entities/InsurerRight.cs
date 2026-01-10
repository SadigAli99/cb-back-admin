
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InsurerRight : BaseEntity
    {
        [StringLength(100)]
        public string? File { get; set; } = null!;
        [StringLength(50)]
        public string? FileType { get; set; } = null!;
        [StringLength(500)]
        public string? Url { get; set; }
        public List<InsurerRightTranslation> Translations { get; set; } = new();
    }
}
