
using CB.Application.DTOs.PostalCommunication;

namespace CB.Application.Interfaces.Services
{
    public interface IPostalCommunicationService
    {
        Task<List<PostalCommunicationGetDTO>> GetAllAsync();
        Task<PostalCommunicationGetDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(PostalCommunicationCreateDTO dto);
        Task<bool> UpdateAsync(int id, PostalCommunicationEditDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
