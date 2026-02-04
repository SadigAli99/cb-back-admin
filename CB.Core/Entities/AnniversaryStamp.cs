
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class AnniversaryStamp : BaseEntity
    {
        [StringLength(100)]
        public string? Image { get; set; }
        public List<AnniversaryStampTranslation>Translations {get; set; } = new();
    }
}
