
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class Logo : BaseEntity
    {
        [StringLength(100)]
        public string? HeaderLogo { get; set; }
        [StringLength(100)]
        public string? FooterLogo { get; set; }
        [StringLength(100)]
        public string? Favicon { get; set; }
    }
}
