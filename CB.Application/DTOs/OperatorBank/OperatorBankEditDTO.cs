
namespace CB.Application.DTOs.OperatorBank
{
    public class OperatorBankEditDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
