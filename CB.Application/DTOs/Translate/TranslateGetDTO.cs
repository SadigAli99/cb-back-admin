

namespace CB.Application.DTOs.Translate
{
    public class TranslateGetDTO
    {
        public int Id { get; set; }
        public string Key { get; set; } = null!;
        public Dictionary<string, string> Values { get; set; } = new();
    }

}
