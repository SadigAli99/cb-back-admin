
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class Social : BaseEntity
    {
        [StringLength(200)]
        public string? Title { get; set; }
        [StringLength(500)]
        public string? Url { get; set; }
        [StringLength(100)]
        public string? Icon { get; set; }

    }
}
