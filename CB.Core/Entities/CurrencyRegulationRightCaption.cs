

namespace CB.Core.Entities
{
    public class CurrencyRegulationRightCaption : BaseEntity
    {
        public List<CurrencyRegulationRightCaptionTranslation> Translations { get; set; } = new();
    }
}
