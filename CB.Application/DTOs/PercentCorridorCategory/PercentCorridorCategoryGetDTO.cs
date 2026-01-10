

using System.ComponentModel.DataAnnotations;

namespace CB.Application.DTOs.PercentCorridorCategory
{
    public class PercentCorridorCategoryGetDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string> Titles { get; set; } = new();
        public Dictionary<string, string> Slugs { get; set; } = new();
        public Dictionary<string, string> Notes { get; set; } = new();
    }

}
