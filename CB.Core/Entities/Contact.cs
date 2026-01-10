
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class Contact : BaseEntity
    {
        [StringLength(100)]
        public string? ContactMail { get; set; }
        [StringLength(10000)]
        public string? Map { get; set; }
        [StringLength(100)]
        public string? ReceptionSchedule { get; set; }
        [StringLength(20)]
        public string? FileSize { get; set; }
        public List<ContactTranslation>? Translations { get; set; }
    }
}
