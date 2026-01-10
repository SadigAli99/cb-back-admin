
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class Office : BaseEntity
    {
        [StringLength(100)]
        public string? Image { get; set; }
        [StringLength(100)]
        public string? Statute { get; set; }
        [StringLength(100)]
        public string? Phone { get; set; }
        public List<OfficeTranslation> Translations { get; set; } = new();
    }
}
