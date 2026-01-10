
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class Permission : BaseEntity
    {
        [StringLength(255)]
        public string? Name { get; set; }
    }
}
