

namespace CB.Core.Entities
{
    public class RegistrationSecurityCaption : BaseEntity
    {
        public List<RegistrationSecurityCaptionTranslation>Translations {get; set; } = new();
    }
}
