

namespace CB.Application.DTOs.Position
{
    public class PositionEditDTO
    {
        public int Id { get; set; }
        public int? BranchId { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
