

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class PolicyAnalysisFileTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int PolicyAnalysisFileId { get; set; }
        public PolicyAnalysisFile? PolicyAnalysisFile { get; set; }
        public int LanguageId { get; set; }
        public Language? Language { get; set; }
    }
}
