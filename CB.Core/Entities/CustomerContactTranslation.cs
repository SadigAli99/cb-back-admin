

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CustomerContactTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(50000)]
        public string? Description { get; set; }
        public int CustomerContactId { get; set; }
        public CustomerContact? CustomerContact { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
