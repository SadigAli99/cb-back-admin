
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CustomerRightCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int CustomerRightCaptionId { get; set; }
        public CustomerRightCaption CustomerRightCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
