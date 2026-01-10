


namespace CB.Application.DTOs.FaqCategory
{
    public class FaqCategoryGetDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
