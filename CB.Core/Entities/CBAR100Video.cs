
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CBAR100Video : BaseEntity
    {
        [StringLength(2000)]
        public string? Url { get; set; }
        public List<CBAR100VideoTranslation> Translations { get; set; } = new();
    }
}
