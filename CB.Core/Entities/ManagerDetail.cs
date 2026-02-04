
namespace CB.Core.Entities
{
    public class ManagerDetail : BaseEntity
    {
        public int ManagerId { get; set; }
        public Manager Manager { get; set; } = null!;
        public List<ManagerDetailTranslation> Translations { get; set; } = new();
    }
}
