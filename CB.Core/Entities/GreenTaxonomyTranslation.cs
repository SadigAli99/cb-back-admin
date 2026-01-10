
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class GreenTaxonomyTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int GreenTaxonomyId { get; set; }
        public GreenTaxonomy GreenTaxonomy { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
