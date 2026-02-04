

namespace CB.Core.Entities
{
    public class BankCaption : BaseEntity
    {
        public List<BankCaptionTranslation>Translations {get; set; } = new();
    }
}
