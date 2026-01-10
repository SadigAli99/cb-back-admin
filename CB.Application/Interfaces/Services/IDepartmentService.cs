using CB.Application.DTOs.Department;

namespace CB.Application.Interfaces.Services
{
    public interface IDepartmentService
    {
        Task<List<DepartmentGetDTO>> GetAllAsync();
        Task<DepartmentGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(DepartmentCreateDTO dto);
        Task<bool> UpdateAsync(int id, DepartmentEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
