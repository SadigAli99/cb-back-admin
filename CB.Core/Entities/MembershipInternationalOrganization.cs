
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class MembershipInternationalOrganization : BaseEntity
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public List<MembershipInternationalOrganizationTranslation> Translations { get; set; } = new();
    }
}
