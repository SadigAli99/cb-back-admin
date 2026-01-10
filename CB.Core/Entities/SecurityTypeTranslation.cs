
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class SecurityTypeTranslation : BaseEntity
    {
        [StringLength(200)]
        public string? Title { get; set; }
        public int SecurityTypeId { get; set; }
        public SecurityType SecurityType { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;

    }
}
