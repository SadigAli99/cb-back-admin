
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class AnniversaryCoin : BaseEntity
    {
        [StringLength(100)]
        public string? Image { get; set; }
        public List<AnniversaryCoinTranslation>? Translations { get; set; }
    }
}
