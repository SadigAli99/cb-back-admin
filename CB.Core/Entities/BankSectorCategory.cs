
namespace CB.Core.Entities
{
    public class BankSectorCategory : BaseEntity
    {
        public List<BankSectorCategoryTranslation> Translations { get; set; } = new();
    }
}
