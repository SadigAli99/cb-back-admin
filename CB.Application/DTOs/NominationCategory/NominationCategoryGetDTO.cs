


namespace CB.Application.DTOs.NominationCategory
{
    public class NominationCategoryGetDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
