

namespace CB.Core.Entities
{
    public class RegistrationSecurity : BaseEntity
    {
        public List<RegistrationSecurityTranslation> Translations { get; set; } = new();
    }
}
