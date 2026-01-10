
namespace CB.Core.Entities
{
    public class ManagerDetail : BaseEntity
    {
        public int ManagerId { get; set; }
        public Manager? Manager { get; set; }
        public List<ManagerDetailTranslation>? Translations { get; set; }
    }
}
