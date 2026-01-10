
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class ComplaintIndex : BaseEntity
    {
        [StringLength(100)]
        public string? File { get; set; } = null!;
        public int Month { get; set; }
        public int Year { get; set; }
        [StringLength(50)]
        public string? FileType { get; set; } = null!;
        public int ComplaintIndexCategoryId { get; set; }
        public ComplaintIndexCategory ComplaintIndexCategory { get; set; } = null!;
        public List<ComplaintIndexTranslation> Translations { get; set; } = new();
    }
}
