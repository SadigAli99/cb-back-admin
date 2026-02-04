
namespace CB.Core.Entities
{
    public class ManagerContact : BaseEntity
    {
        public int ManagerId { get; set; }
        public Manager Manager { get; set; } = null!;
        public List<ManagerContactTranslation> Translations { get; set; } = new();
    }
}
