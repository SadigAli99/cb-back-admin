

namespace CB.Core.Entities
{
    public class NonBankCaption : BaseEntity
    {
        public List<NonBankCaptionTranslation>Translations {get; set; } = new();
    }
}
