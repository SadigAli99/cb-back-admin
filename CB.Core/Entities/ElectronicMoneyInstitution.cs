

namespace CB.Core.Entities
{
    public class ElectronicMoneyInstitution : BaseEntity
    {
        public List<ElectronicMoneyInstitutionTranslation> Translations { get; set; } = new();
    }
}
