
using CB.Application.DTOs.Interview;

namespace CB.Application.Interfaces.Services
{
    public interface IInterviewService
    {
        Task<List<InterviewGetDTO>> GetAllAsync();
        Task<InterviewGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(InterviewCreateDTO dto);
        Task<bool> UpdateAsync(int id, InterviewEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
