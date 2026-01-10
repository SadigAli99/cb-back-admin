

namespace CB.Application.DTOs.Branch
{
    public class BranchEditDTO
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
