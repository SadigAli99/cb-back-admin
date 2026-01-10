
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class InternationalEventImage : BaseEntity
    {
        [StringLength(100)]
        public string? Image { get; set; }
        public int InternationalEventId { get; set; }
        public InternationalEvent InternationalEvent { get; set; } = null!;
    }
}
