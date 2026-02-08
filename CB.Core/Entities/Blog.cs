
using System.ComponentModel.DataAnnotations.Schema;

namespace CB.Core.Entities
{
    public class Blog : BaseEntity
    {
        [Column("date")]
        public DateTime Date { get; set; }
        public string? Image { get; set; } = null!;
        public bool IsFocused { get; set; } = false;
        public List<BlogTranslation> Translations { get; set; } = new();
        public List<BlogImage> Images { get; set; } = new();
    }
}
