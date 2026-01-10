
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class BlogImage : BaseEntity
    {
        [StringLength(100)]
        public string Image { get; set; } = null!;
        public int BlogId { get; set; }
        public Blog Blog { get; set; } = null!;
    }
}
