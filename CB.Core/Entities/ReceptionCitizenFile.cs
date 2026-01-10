
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class ReceptionCitizenFile : BaseEntity
    {
        [StringLength(100)]
        public string? File { get; set; } = null!;
        [StringLength(50)]
        public string? FileType { get; set; } = null!;
        public int ReceptionCitizenId { get; set; }
        public ReceptionCitizen ReceptionCitizen { get; set; } = null!;
        public List<ReceptionCitizenFileTranslation> Translations { get; set; } = new();
    }
}
