
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class NakhchivanEventImage : BaseEntity
    {
        [StringLength(100)]
        public string Image { get; set; } = null!;
        public int NakhchivanEventId { get; set; }
        public NakhchivanEvent NakhchivanEvent { get; set; } = null!;
    }
}
