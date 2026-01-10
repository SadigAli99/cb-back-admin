

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class ReceptionCitizenFileTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int ReceptionCitizenFileId { get; set; }
        public ReceptionCitizenFile ReceptionCitizenFile { get; set; } = null!;
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
