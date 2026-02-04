
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class Hero : BaseEntity
    {
        [StringLength(100)]
        public string? Image { get; set; }
        public List<HeroTranslation> Translations { get; set; } = new();
    }
}
