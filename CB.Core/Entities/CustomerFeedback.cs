
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CustomerFeedback : BaseEntity
    {
        [StringLength(500)]
        public string? Url { get; set; }
        public List<CustomerFeedbackTranslation> Translations { get; set; } = new();
    }
}
