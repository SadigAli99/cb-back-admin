


namespace CB.Application.DTOs.IssuerType
{
    public class IssuerTypeGetDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
