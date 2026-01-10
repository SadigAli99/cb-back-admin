
namespace CB.Core.Entities
{
    public class ManagerContact : BaseEntity
    {
        public int ManagerId { get; set; }
        public Manager? Manager { get; set; }
        public List<ManagerContactTranslation>? Translations { get; set; }
    }
}
