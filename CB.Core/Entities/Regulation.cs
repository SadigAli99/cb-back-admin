
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class Regulation : BaseEntity
    {
        [StringLength(100)]
        public string? File { get; set; } = null!;
        [StringLength(50)]
        public string? FileType { get; set; } = null!;
        public List<RegulationTranslation> Translations { get; set; } = new();
    }
}
