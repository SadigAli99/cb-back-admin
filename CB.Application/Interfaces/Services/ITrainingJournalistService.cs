
using CB.Application.DTOs.TrainingJournalist;

namespace CB.Application.Interfaces.Services
{
    public interface ITrainingJournalistService
    {
        Task<bool> CreateOrUpdate(TrainingJournalistPostDTO dTO);
        Task<TrainingJournalistGetDTO?> GetFirst();
        Task<bool> DeleteImageAsync(int id, int imageId);
    }
}
