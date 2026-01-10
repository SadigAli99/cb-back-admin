


namespace CB.Application.DTOs.Department
{
    public class DepartmentGetDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
