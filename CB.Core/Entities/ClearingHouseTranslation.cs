
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class ClearingHouseTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int ClearingHouseId { get; set; }
        public ClearingHouse ClearingHouse { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
