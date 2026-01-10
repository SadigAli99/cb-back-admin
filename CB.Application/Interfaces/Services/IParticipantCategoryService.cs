using CB.Application.DTOs.ParticipantCategory;

namespace CB.Application.Interfaces.Services
{
    public interface IParticipantCategoryService
    {
        Task<List<ParticipantCategoryGetDTO>> GetAllAsync();
        Task<ParticipantCategoryGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(ParticipantCategoryCreateDTO dto);
        Task<bool> UpdateAsync(int id, ParticipantCategoryEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
