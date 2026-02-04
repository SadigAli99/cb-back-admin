

namespace CB.Core.Entities
{
    public class FinancialInstitution : BaseEntity
    {
        public List<FinancialInstitutionTranslation>Translations {get; set; } = new();
    }
}
