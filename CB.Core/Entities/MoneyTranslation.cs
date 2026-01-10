
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class MoneyTranslation : BaseEntity
    {
        [StringLength(255)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? Topic { get; set; }
        [StringLength(50)]
        public string? ReleaseDate { get; set; }
        public int MoneyId { get; set; }
        public Money Money { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
