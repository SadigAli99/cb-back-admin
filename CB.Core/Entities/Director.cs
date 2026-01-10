
using CB.Core.Enums;

namespace CB.Core.Entities
{
    public class Director : BaseEntity
    {
        public string? Image { get; set; }
        public DirectorType? Type { get; set; }
        public List<DirectorTranslation>? Translations { get; set; }
        public List<DirectorContact>? Contacts { get; set; }
        public List<DirectorDetail>? Details { get; set; }
    }
}
