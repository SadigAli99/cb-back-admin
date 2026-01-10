
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class MeasFile : BaseEntity
    {
        [StringLength(100)]
        public string? File { get; set; } = null!;
        [StringLength(50)]
        public string? FileType { get; set; } = null!;
        public List<MeasFileTranslation> Translations { get; set; } = new();
    }
}
