
namespace CB.Application.DTOs.Translate
{
    public class TranslateCreateDTO
{
    public string Key { get; set; } = null!;
    public Dictionary<string, string> Values { get; set; } = new();
}

}
