
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CBAR105EventImage : BaseEntity
    {
        [StringLength(100)]
        public string Image { get; set; } = null!;
        public int CBAR105EventId { get; set; }
        public CBAR105Event CBAR105Event { get; set; } = null!;
    }
}
