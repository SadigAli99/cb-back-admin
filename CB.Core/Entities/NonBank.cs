

namespace CB.Core.Entities
{
    public class NonBank : BaseEntity
    {
        public List<NonBankTranslation> Translations { get; set; } = new();
    }
}
