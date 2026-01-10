
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CustomerEventImage : BaseEntity
    {
        [StringLength(100)]
        public string Image { get; set; } = null!;
        public int CustomerEventId { get; set; }
        public CustomerEvent CustomerEvent { get; set; } = null!;
    }
}
