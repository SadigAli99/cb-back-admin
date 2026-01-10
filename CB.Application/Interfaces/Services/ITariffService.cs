
using CB.Application.DTOs.Tariff;

namespace CB.Application.Interfaces.Services
{
    public interface ITariffService
    {
        Task<List<TariffGetDTO>> GetAllAsync();
        Task<TariffGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(TariffCreateDTO dto);
        Task<bool> UpdateAsync(int id, TariffEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
