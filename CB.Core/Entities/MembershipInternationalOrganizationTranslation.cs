

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class MembershipInternationalOrganizationTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int MembershipInternationalOrganizationId { get; set; }
        public MembershipInternationalOrganization? MembershipInternationalOrganization { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
