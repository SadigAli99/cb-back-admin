
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class EconometricModelTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int EconometricModelId { get; set; }
        public EconometricModel EconometricModel { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
