
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class Methodology : BaseEntity
    {
        [StringLength(100)]
        public string? File { get; set; } = null!;
        [StringLength(50)]
        public string? FileType { get; set; } = null!;
        public List<MethodologyTranslation> Translations { get; set; } = new();
    }
}
