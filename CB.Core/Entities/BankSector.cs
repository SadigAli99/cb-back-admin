
using System.ComponentModel.DataAnnotations.Schema;

namespace CB.Core.Entities
{
    public class BankSector : BaseEntity
    {
        [Column("date")]
        public DateTime Date { get; set; }
        public double Value { get; set; }
        public int BankSectorCategoryId { get; set; }

        public BankSectorCategory BankSectorCategory { get; set; } = null!;
    }
}
