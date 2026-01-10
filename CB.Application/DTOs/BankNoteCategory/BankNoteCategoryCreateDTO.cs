

namespace CB.Application.DTOs.BankNoteCategory
{
    public class BankNoteCategoryCreateDTO
    {
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> ShortTitles { get; set; } = new();
        public Dictionary<string, string> Slugs { get; set; } = new();
        public Dictionary<string, string> Notes { get; set; } = new();
    }

}
