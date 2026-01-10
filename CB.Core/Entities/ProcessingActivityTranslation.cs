
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class ProcessingActivityTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int ProcessingActivityId { get; set; }
        public ProcessingActivity ProcessingActivity { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
