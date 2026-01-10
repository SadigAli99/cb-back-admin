
using CB.Application.DTOs.VirtualEducation;

namespace CB.Application.Interfaces.Services
{
    public interface IVirtualEducationService
    {
        Task<List<VirtualEducationGetDTO>> GetAllAsync();
        Task<VirtualEducationGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(VirtualEducationCreateDTO dto);
        Task<bool> UpdateAsync(int id, VirtualEducationEditDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteImageAsync(int id, int imageId);
    }
}
