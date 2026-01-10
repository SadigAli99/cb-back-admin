


namespace CB.Application.DTOs.Position
{
    public class PositionGetDTO
    {
        public int Id { get; set; }
        public string? Branch { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
