
namespace CB.Core.Entities
{
    public class Position : BaseEntity
    {
        public int? BranchId { get; set; }
        public Branch? Branch { get; set; }
        public List<PositionTranslation> Translations { get; set; } = new();
    }
}
