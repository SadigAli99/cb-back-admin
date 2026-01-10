
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class TrainingJournalistImage : BaseEntity
    {
        [StringLength(100)]
        public string Image { get; set; } = null!;
        public int TrainingJournalistId { get; set; }
        public TrainingJournalist TrainingJournalist { get; set; } = null!;
    }
}
