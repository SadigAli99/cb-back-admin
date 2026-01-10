using CB.Application.DTOs.ComplaintIndexCategory;

namespace CB.Application.Interfaces.Services
{
    public interface IComplaintIndexCategoryService
    {
        Task<List<ComplaintIndexCategoryGetDTO>> GetAllAsync();
        Task<ComplaintIndexCategoryGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(ComplaintIndexCategoryCreateDTO dto);
        Task<bool> UpdateAsync(int id, ComplaintIndexCategoryEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
