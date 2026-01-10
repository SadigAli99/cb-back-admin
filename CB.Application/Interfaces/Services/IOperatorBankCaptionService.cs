
using CB.Application.DTOs.OperatorBankCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IOperatorBankCaptionService
    {
        Task<bool> CreateOrUpdate(OperatorBankCaptionPostDTO dTO);
        Task<OperatorBankCaptionGetDTO> GetFirst();
    }
}
