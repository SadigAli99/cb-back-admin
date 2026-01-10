
namespace CB.Core.Entities
{
    public class NakhchivanBlog : BaseEntity
    {
        public List<NakhchivanBlogTranslation> Translations { get; set; } = new();
        public List<NakhchivanBlogImage>? Images { get; set; } = new();
    }
}
