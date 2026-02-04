

namespace CB.Core.Entities
{
    public class CapitalMarketRightCaption : BaseEntity
    {
        public List<CapitalMarketRightCaptionTranslation>Translations {get; set; } = new();
    }
}
