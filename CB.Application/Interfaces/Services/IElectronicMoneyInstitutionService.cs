
using CB.Application.DTOs.ElectronicMoneyInstitution;

namespace CB.Application.Interfaces.Services
{
    public interface IElectronicMoneyInstitutionService
    {
        Task<List<ElectronicMoneyInstitutionGetDTO>> GetAllAsync();
        Task<ElectronicMoneyInstitutionGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(ElectronicMoneyInstitutionCreateDTO dto);
        Task<bool> UpdateAsync(int id, ElectronicMoneyInstitutionEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
