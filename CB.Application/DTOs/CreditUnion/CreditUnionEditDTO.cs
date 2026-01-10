

namespace CB.Application.DTOs.CreditUnion
{
    public class CreditUnionEditDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
