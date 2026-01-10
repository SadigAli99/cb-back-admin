
namespace CB.Core.Entities
{
    public class IssuerType : BaseEntity
    {
        public List<IssuerTypeTranslation> Translations { get; set; } = new();
    }
}
