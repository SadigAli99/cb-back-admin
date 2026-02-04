
using CB.Application.DTOs.StatisticCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IStatisticCaptionService
    {
        Task<bool> CreateOrUpdate(StatisticCaptionPostDTO dTO);
        Task<StatisticCaptionGetDTO?> GetFirst();
    }
}
