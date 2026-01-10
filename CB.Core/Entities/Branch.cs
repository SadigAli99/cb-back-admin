
namespace CB.Core.Entities
{
    public class Branch : BaseEntity
    {
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
        public List<BranchTranslation> Translations { get; set; } = new();
    }
}
