using CB.Application.DTOs.InformationType;

namespace CB.Application.Interfaces.Services
{
    public interface IInformationTypeService
    {
        Task<List<InformationTypeGetDTO>> GetAllAsync();
        Task<InformationTypeGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(InformationTypeCreateDTO dto);
        Task<bool> UpdateAsync(int id, InformationTypeEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
