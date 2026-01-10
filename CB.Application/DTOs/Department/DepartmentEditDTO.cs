

namespace CB.Application.DTOs.Department
{
    public class DepartmentEditDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
    }

}
