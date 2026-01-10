
namespace CB.Application.DTOs.OperatorBank
{
    public class OperatorBankGetDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }
}
