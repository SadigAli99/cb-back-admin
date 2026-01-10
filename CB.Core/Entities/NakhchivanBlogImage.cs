
using System.ComponentModel.DataAnnotations;

namespace CB.Core.Entities
{
    public class NakhchivanBlogImage : BaseEntity
    {
        [StringLength(100)]
        public string Image { get; set; } = null!;
        public int NakhchivanBlogId { get; set; }
        public NakhchivanBlog NakhchivanBlog { get; set; } = null!;
    }
}
