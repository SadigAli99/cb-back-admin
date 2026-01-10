
using CB.Application.DTOs.CentralBankCooperation;

namespace CB.Application.Interfaces.Services
{
    public interface ICentralBankCooperationService
    {
        Task<List<CentralBankCooperationGetDTO>> GetAllAsync();
        Task<CentralBankCooperationGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CentralBankCooperationCreateDTO dto);
        Task<bool> UpdateAsync(int id, CentralBankCooperationEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
