using CB.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;

namespace CB.Application.DTOs.PercentCorridor
{
    public class PercentCorridorEditDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Value { get; set; }
        public int PercentCorridorCategoryId { get; set; }

    }
}
