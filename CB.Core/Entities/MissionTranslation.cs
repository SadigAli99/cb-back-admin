

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class MissionTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(2000)]
        public string? Text { get; set; }
        public int MissionId { get; set; }
        public Mission? Mission { get; set; }
        public int LanguageId { get; set; }
        public Language? Language { get; set; }
    }
}
