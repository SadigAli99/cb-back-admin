
namespace CB.Application.DTOs.Translate
{
    public class TranslateEditDTO
{
    public int Id { get; set; }
    public string Key { get; set; } = null!;
    public Dictionary<string, string> Values { get; set; } = new();
}

}
