
namespace CB.Core.Entities
{
    public class BankNoteCategory : BaseEntity
    {
        public List<BankNoteCategoryTranslation> Translations { get; set; } = new();
    }
}
