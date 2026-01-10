
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class PageDetail : BaseEntity
    {
        [StringLength(200)]
        public string? Key { get; set; }
        [StringLength(100)]
        public string? Image { get; set; }
        public int PageId { get; set; }
        public Page Page { get; set; } = null!;
        public List<PageDetailTranslation> Translations { get; set; } = new();

    }
}
