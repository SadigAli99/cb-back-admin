
using CB.Application.DTOs.ForeignInsuranceBroker;

namespace CB.Application.Interfaces.Services
{
    public interface IForeignInsuranceBrokerService
    {
        Task<List<ForeignInsuranceBrokerGetDTO>> GetAllAsync();
        Task<ForeignInsuranceBrokerGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(ForeignInsuranceBrokerCreateDTO dto);
        Task<bool> UpdateAsync(int id, ForeignInsuranceBrokerEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
