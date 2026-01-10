
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class Nomination : BaseEntity
    {
        [StringLength(100)]
        public string? File { get; set; } = null!;
        [StringLength(50)]
        public string? FileType { get; set; } = null!;
        public int NominationCategoryId { get; set; }
        public NominationCategory NominationCategory { get; set; } = null!;
        public List<NominationTranslation> Translations { get; set; } = new();
    }
}
