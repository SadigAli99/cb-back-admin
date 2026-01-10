
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class EKYCTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int EKYCId { get; set; }
        public EKYC EKYC { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
