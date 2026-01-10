
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InsurerFile : BaseEntity
    {
        [StringLength(100)]
        public string? File { get; set; } = null!;
        [StringLength(50)]
        public string? FileType { get; set; } = null!;
        public List<InsurerFileTranslation> Translations { get; set; } = new();
    }
}
