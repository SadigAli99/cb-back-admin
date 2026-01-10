

namespace CB.Application.DTOs.Branch
{
    public class BranchCreateDTO
    {
        public int DepartmentId { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
