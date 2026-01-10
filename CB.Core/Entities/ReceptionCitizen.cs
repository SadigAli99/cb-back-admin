
namespace CB.Core.Entities
{
    public class ReceptionCitizen : BaseEntity
    {
        public int? ReceptionCitizenCategoryId { get; set; }
        public ReceptionCitizenCategory? ReceptionCitizenCategory { get; set; }
        public List<ReceptionCitizenTranslation> Translations { get; set; } = new();
        public List<ReceptionCitizenFile> Files { get; set; } = new();
    }
}
