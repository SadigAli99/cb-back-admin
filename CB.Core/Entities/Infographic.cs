
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class Infographic : BaseEntity
    {
        [StringLength(100)]
        public string? File { get; set; } = null!;
        public int Month { get; set; }
        public int Year { get; set; }
        [StringLength(50)]
        public string? FileType { get; set; } = null!;
        public List<InfographicTranslation> Translations { get; set; } = new();
    }
}
