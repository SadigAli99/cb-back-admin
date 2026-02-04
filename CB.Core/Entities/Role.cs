using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class Role : BaseEntity
    {
        [StringLength(50)]
        public string? Name { get; set; }
        public List<RolePermission> Permissions { get; set; } = new();
    }
}
