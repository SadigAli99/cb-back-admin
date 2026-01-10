

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class MacroprudentialPolicyFrameworkTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int MacroprudentialPolicyFrameworkId { get; set; }
        public MacroprudentialPolicyFramework? MacroprudentialPolicyFramework { get; set; }
        public int LanguageId { get; set; }
        public Language? Language { get; set; }
    }
}
