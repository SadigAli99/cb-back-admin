
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class HeroTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int HeroId { get; set; }
        public Hero Hero { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
