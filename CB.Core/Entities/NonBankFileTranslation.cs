

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class NonBankFileTranslation : BaseEntity
    {
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? CoverTitle { get; set; }
        public int NonBankFileId { get; set; }
        public NonBankFile? NonBankFile { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
