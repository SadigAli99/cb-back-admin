
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class IssuerTypeTranslation : BaseEntity
    {
        [StringLength(200)]
        public string? Title { get; set; }
        public int IssuerTypeId { get; set; }
        public IssuerType IssuerType { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;

    }
}
