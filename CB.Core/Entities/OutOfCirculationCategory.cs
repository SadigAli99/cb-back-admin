

using System.ComponentModel.DataAnnotations;
using CB.Core.Enums;

namespace CB.Core.Entities
{
    public class OutOfCirculationCategory : BaseEntity
    {
        [StringLength(100)]
        public string? Image { get; set; }
        public MoneyType Type { get; set; }
        public List<OutOfCirculationCategoryTranslation> Translations { get; set; } = new();

    }
}
