
using CB.Application.DTOs.Contact;

namespace CB.Application.Interfaces.Services
{
    public interface IContactService
    {
        Task<bool> CreateOrUpdate(ContactPostDTO dTO);
        Task<ContactGetDTO> GetFirst();
    }
}
