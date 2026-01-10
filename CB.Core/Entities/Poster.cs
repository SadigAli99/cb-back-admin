

using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class Poster : BaseEntity
    {
        [StringLength(100)]
        public string? Image { get; set; }
    }
}
