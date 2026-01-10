
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CustomerFeedbackTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int CustomerFeedbackId { get; set; }
        public CustomerFeedback CustomerFeedback { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
