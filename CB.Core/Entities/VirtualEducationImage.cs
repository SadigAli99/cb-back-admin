
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class VirtualEducationImage : BaseEntity
    {
        [StringLength(100)]
        public string? Image { get; set; }
        public int VirtualEducationId { get; set; }
        public VirtualEducation VirtualEducation { get; set; } = null!;
    }
}
