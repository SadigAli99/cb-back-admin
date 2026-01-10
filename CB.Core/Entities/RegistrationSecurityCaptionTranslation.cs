
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class RegistrationSecurityCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int RegistrationSecurityCaptionId { get; set; }
        public RegistrationSecurityCaption RegistrationSecurityCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
