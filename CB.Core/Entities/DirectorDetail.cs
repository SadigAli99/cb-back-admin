
namespace CB.Core.Entities
{
    public class DirectorDetail : BaseEntity
    {
        public int DirectorId { get; set; }
        public Director? Director { get; set; }
        public List<DirectorDetailTranslation>? Translations { get; set; }
    }
}
