
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class Page : BaseEntity
    {
        [StringLength(200)]
        public string? Key { get; set; }
        [StringLength(100)]
        public string? Image { get; set; }
        public List<PageDetail> Details { get; set; } = new();
        public List<PageTranslation> Translations { get; set; } = new();

    }
}
