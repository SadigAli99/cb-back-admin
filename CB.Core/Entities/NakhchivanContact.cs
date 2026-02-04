

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class NakhchivanContact : BaseEntity
    {
        [StringLength(50000)]
        public string? Map { get; set; }
        public List<NakhchivanContactTranslation>Translations {get; set; } = new();
    }
}
