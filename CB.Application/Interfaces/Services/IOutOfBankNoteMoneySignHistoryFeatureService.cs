
using CB.Application.DTOs.OutOfBankNoteMoneySignHistoryFeature;

namespace CB.Application.Interfaces.Services
{
    public interface IOutOfBankNoteMoneySignHistoryFeatureService
    {
        Task<bool> CreateOrUpdate(OutOfBankNoteMoneySignHistoryFeaturePostDTO dTO);
        Task<OutOfBankNoteMoneySignHistoryFeatureGetDTO> GetFirst();
    }
}
