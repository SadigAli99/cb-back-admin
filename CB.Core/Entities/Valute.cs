
using System.ComponentModel.DataAnnotations;
using CB.Core.Enums;

namespace CB.Core.Entities
{
    public class Valute : BaseEntity
    {
        [StringLength(100)]
        public string Unit { get; set; } = null!;
        [StringLength(10)]
        public string Code { get; set; } = null!;
        public ValuteType Type { get; set; }
        public bool InHome { get; set; } = false;
        public List<ValuteTranslation> Translations { get; set; } = new();
    }
}
