
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class ValuteTranslation : BaseEntity
    {
        [StringLength(100)]
        public string? Title { get; set; }
        public int ValuteId { get; set; }
        public Valute Valute { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
