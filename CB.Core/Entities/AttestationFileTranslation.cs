

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class AttestationFileTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        public int AttestationFileId { get; set; }
        public AttestationFile? AttestationFile { get; set; }
        public int LanguageId { get; set; }
        public Language? Language { get; set; }
    }
}
