using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CB.Application.DTOs.Inflation;

namespace CB.Application.Interfaces.Services
{
    public interface IInflationService
    {
        Task<List<InflationGetDTO>> GetAllAsync();
        Task<InflationGetDTO?> GetByIdAsync(int id);
        Task<InflationEditDTO?> GetForEditAsync(int id);
        Task<bool> CreateAsync(InflationCreateDTO dto);
        Task<bool> UpdateAsync(int id, InflationEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
