
using CB.Application.DTOs.InformationMemorandumCaption;

namespace CB.Application.Interfaces.Services
{
    public interface IInformationMemorandumCaptionService
    {
        Task<bool> CreateOrUpdate(InformationMemorandumCaptionPostDTO dTO);
        Task<InformationMemorandumCaptionGetDTO?> GetFirst();
    }
}
