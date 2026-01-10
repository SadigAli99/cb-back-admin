
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class VolunteerTranslation : BaseEntity
    {
        [StringLength(100)]
        public string? Fullname { get; set; }
        [StringLength(20000)]
        public string? Description { get; set; }
        public int VolunteerId { get; set; }
        public Volunteer? Volunteer { get; set; }
        public int LanguageId { get; set; }
        public Language? Language { get; set; }
    }
}
