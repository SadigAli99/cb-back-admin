
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class NakhchivanContactTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int NakhchivanContactId { get; set; }
        public NakhchivanContact NakhchivanContact { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
