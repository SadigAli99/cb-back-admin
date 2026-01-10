
namespace CB.Core.Entities
{
    public class Department : BaseEntity
    {
        public List<DepartmentTranslation> Translations { get; set; } = new();
    }
}
