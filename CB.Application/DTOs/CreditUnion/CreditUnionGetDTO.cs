
namespace CB.Application.DTOs.CreditUnion
{
    public class CreditUnionGetDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
