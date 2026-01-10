
using System.ComponentModel.DataAnnotations;
using CB.Core.Enums;

namespace CB.Core.Entities
{
    public class EventMedia : BaseEntity
    {
        [StringLength(100)]
        public string? Url { get; set; }
        public MediaType MediaType { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; } = null!;
    }
}
