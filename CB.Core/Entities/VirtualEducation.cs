
namespace CB.Core.Entities
{
    public class VirtualEducation : BaseEntity
    {
        public List<VirtualEducationTranslation> Translations { get; set; } = new();
        public List<VirtualEducationImage> Images { get; set; } = new();
    }
}
