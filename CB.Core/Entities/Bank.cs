

namespace CB.Core.Entities
{
    public class Bank : BaseEntity
    {
        public List<BankTranslation> Translations { get; set; } = new();
    }
}
