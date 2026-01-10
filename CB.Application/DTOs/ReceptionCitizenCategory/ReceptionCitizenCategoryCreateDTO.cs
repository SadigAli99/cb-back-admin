

namespace CB.Application.DTOs.ReceptionCitizenCategory
{
    public class ReceptionCitizenCategoryCreateDTO
    {
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string,string> Descriptions { get; set; } = new();
    }

}
