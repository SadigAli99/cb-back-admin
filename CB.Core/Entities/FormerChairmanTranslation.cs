
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class FormerChairmanTranslation : BaseEntity
    {
        [StringLength(100)]
        public string? Fullname { get; set; }
        [StringLength(50000)]
        public string? Description { get; set; }
        public int FormerChairmanId { get; set; }
        public FormerChairman FormerChairman { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
