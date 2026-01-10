
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class ActuaryCaptionTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int ActuaryCaptionId { get; set; }
        public ActuaryCaption ActuaryCaption { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
