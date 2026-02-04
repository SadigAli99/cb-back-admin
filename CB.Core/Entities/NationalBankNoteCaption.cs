

namespace CB.Core.Entities
{
    public class NationalBankNoteCaption : BaseEntity
    {
        public List<NationalBankNoteCaptionTranslation>Translations {get; set; } = new();
    }
}
