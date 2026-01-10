using System;
using CB.Core.Enums;

namespace CB.Application.DTOs.PercentCorridor
{
    public class PercentCorridorGetDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Value { get; set; }
        public string? PercentCategoryTitle { get; set; }
    }
}
