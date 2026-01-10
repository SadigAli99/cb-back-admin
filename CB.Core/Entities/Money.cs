
using System.ComponentModel.DataAnnotations;
using CB.Core.Enums;

namespace CB.Core.Entities
{
    public class Money : BaseEntity
    {
        public MoneyType Type { get; set; }
        [StringLength(100)]
        public string? Image { get; set; }
        public List<MoneyTranslation> Translations { get; set; } = new();
    }
}
