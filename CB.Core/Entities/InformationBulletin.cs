
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InformationBulletin : BaseEntity
    {
        [StringLength(100)]
        public string? File { get; set; } = null!;
        public int Year { get; set; }
        [StringLength(50)]
        public string? FileType { get; set; } = null!;
        public List<InformationBulletinTranslation> Translations { get; set; } = new();
    }
}
