
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CustomerDocumentFile : BaseEntity
    {
        [StringLength(100)]
        public string? File { get; set; } = null!;
        [StringLength(50)]
        public string? FileType { get; set; } = null!;
        public int CustomerDocumentId { get; set; }
        public CustomerDocument CustomerDocument { get; set; } = null!;
        public List<CustomerDocumentFileTranslation> Translations { get; set; } = new();
    }
}
