
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class CBAR100Gallery : BaseEntity
    {
        [StringLength(100)]
        public string? Image { get; set; }
    }
}
