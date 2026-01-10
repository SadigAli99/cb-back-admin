
namespace CB.Core.Entities
{
    public class ReceptionCitizenCategory : BaseEntity
    {
        public List<ReceptionCitizen> Citizens { get; set; } = new();
        public List<ReceptionCitizenCategoryTranslation> Translations { get; set; } = new();
    }
}
