

namespace CB.Core.Entities
{
    public class CBAR105Caption : BaseEntity
    {
        public List<CBAR105CaptionTranslation> Translations { get; set; } = new();
    }
}
