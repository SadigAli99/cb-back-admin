

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InterviewTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int InterviewId { get; set; }
        public Interview? Interview { get; set; }
        public int LanguageId { get; set; }
        public Language? Language { get; set; }
    }
}
