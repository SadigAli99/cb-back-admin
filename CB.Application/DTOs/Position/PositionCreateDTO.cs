

namespace CB.Application.DTOs.Position
{
    public class PositionCreateDTO
    {
        public int? BranchId { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
