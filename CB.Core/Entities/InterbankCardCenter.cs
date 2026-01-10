

namespace CB.Core.Entities
{
    public class InterbankCardCenter : BaseEntity
    {
        public List<InterbankCardCenterTranslation> Translations { get; set; } = new();
    }
}
