
using CB.Application.DTOs.ProcessingActivity;

namespace CB.Application.Interfaces.Services
{
    public interface IProcessingActivityService
    {
        Task<bool> CreateOrUpdate(ProcessingActivityPostDTO dTO);
        Task<ProcessingActivityGetDTO> GetFirst();
    }
}
