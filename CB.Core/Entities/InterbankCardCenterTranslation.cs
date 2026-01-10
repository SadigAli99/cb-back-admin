
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InterbankCardCenterTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int InterbankCardCenterId { get; set; }
        public InterbankCardCenter InterbankCardCenter { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
