

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CustomerDocumentFileTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int CustomerDocumentFileId { get; set; }
        public CustomerDocumentFile CustomerDocumentFile { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
