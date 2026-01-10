
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class PresidentDecreeTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int PresidentDecreeId { get; set; }
        public PresidentDecree PresidentDecree { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
