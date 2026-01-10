

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class ComplaintIndexCategoryTranslation : BaseEntity
    {
        [StringLength(255)]
        public string? Title { get; set; } = null!;

        public int ComplaintIndexCategoryId { get; set; }
        public ComplaintIndexCategory ComplaintIndexCategory { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
