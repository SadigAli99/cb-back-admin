
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class ChronologyTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int ChronologyId { get; set; }
        public Chronology Chronology { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
