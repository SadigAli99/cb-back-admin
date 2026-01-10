
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class EventContentImage : BaseEntity
    {
        [StringLength(100)]
        public string? Image { get; set; }
        public int EventContentId { get; set; }
        public EventContent EventContent { get; set; } = null!;
    }
}
