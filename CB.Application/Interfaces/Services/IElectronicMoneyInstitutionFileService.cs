
using CB.Application.DTOs.ElectronicMoneyInstitutionFile;

namespace CB.Application.Interfaces.Services
{
    public interface IElectronicMoneyInstitutionFileService
    {
        Task<List<ElectronicMoneyInstitutionFileGetDTO>> GetAllAsync();
        Task<ElectronicMoneyInstitutionFileGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(ElectronicMoneyInstitutionFileCreateDTO dto);
        Task<bool> UpdateAsync(int id, ElectronicMoneyInstitutionFileEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
