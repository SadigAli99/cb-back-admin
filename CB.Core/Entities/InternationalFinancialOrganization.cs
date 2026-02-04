

namespace CB.Core.Entities
{
    public class InternationalFinancialOrganization : BaseEntity
    {
        public List<InternationalFinancialOrganizationTranslation>Translations {get; set; } = new();
    }
}
