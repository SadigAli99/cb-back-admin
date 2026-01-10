
using CB.Application.DTOs.NationalBankNoteCaption;

namespace CB.Application.Interfaces.Services
{
    public interface INationalBankNoteCaptionService
    {
        Task<bool> CreateOrUpdate(NationalBankNoteCaptionPostDTO dTO);
        Task<NationalBankNoteCaptionGetDTO> GetFirst();
    }
}
