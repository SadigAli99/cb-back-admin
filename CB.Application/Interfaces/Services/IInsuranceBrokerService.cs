
using CB.Application.DTOs.InsuranceBroker;

namespace CB.Application.Interfaces.Services
{
    public interface IInsuranceBrokerService
    {
        Task<List<InsuranceBrokerGetDTO>> GetAllAsync();
        Task<InsuranceBrokerGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(InsuranceBrokerCreateDTO dto);
        Task<bool> UpdateAsync(int id, InsuranceBrokerEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
