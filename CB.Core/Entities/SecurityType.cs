
namespace CB.Core.Entities
{
    public class SecurityType : BaseEntity
    {
        public List<SecurityTypeTranslation> Translations { get; set; } = new();
    }
}
