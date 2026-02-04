

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CustomerDocumentTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(50000)]
        public string? Description { get; set; }
        public int CustomerDocumentId { get; set; }
        public CustomerDocument? CustomerDocument { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = new();
    }
}
