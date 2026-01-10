

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class PolicyConceptTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int PolicyConceptId { get; set; }
        public PolicyConcept? PolicyConcept { get; set; }
        public int LanguageId { get; set; }
        public Language? Language { get; set; }
    }
}
