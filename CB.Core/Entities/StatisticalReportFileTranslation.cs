

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class StatisticalReportFileTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int StatisticalReportFileId { get; set; }
        public StatisticalReportFile? StatisticalReportFile { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
