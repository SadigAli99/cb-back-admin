
namespace CB.Core.Entities
{
    public class InformationType : BaseEntity
    {
        public List<InformationTypeTranslation> Translations { get; set; } = new();
    }
}
