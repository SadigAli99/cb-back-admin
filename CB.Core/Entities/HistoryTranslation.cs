
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class HistoryTranslation : BaseEntity
    {
        [StringLength(200)]
        public string? Title { get; set; }
        [StringLength(20000)]
        public string? Description { get; set; }
        public int HistoryId { get; set; }
        public History History { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
