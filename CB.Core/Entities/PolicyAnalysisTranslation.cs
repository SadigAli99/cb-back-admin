
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class PolicyAnalysisTranslation : BaseEntity
    {
        [StringLength(50000)]
        public string? Description { get; set; }
        public int PolicyAnalysisId { get; set; }
        public PolicyAnalysis PolicyAnalysis { get; set; } = null!;

        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
