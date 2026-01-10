


namespace CB.Application.DTOs.Branch
{
    public class BranchGetDTO
    {
        public int Id { get; set; }
        public string? Department { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
