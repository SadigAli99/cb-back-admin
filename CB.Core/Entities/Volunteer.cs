
using CB.Core.Enums;

namespace CB.Core.Entities
{
    public class Volunteer : BaseEntity
    {
        public string? Image { get; set; }
        public List<VolunteerTranslation>? Translations { get; set; }
    }
}
