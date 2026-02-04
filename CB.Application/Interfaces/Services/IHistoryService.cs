
using CB.Application.DTOs.History;

namespace CB.Application.Interfaces.Services
{
    public interface IHistoryService
    {
        Task<bool> CreateOrUpdate(HistoryPostDTO dTO);
        Task<HistoryGetDTO?> GetFirst();
    }
}
