using CB.Application.DTOs.ReceptionCitizenCategory;

namespace CB.Application.Interfaces.Services
{
    public interface IReceptionCitizenCategoryService
    {
        Task<List<ReceptionCitizenCategoryGetDTO>> GetAllAsync();
        Task<ReceptionCitizenCategoryGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(ReceptionCitizenCategoryCreateDTO dto);
        Task<bool> UpdateAsync(int id, ReceptionCitizenCategoryEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
