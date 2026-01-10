
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InformationTypeTranslation : BaseEntity
    {
        [StringLength(200)]
        public string? Title { get; set; }
        public int InformationTypeId { get; set; }
        public InformationType InformationType { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;

    }
}
