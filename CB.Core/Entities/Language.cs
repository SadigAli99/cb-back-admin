
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class Language : BaseEntity
    {
        [StringLength(50)]
        public string Name { get; set; } = null!;
        [StringLength(10)]
        public string Code { get; set; } = null!;
    }
}
