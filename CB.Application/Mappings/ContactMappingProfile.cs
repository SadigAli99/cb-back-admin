
using CB.Application.DTOs.Contact;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class ContactMappingProfile : MappingProfile
    {
        public ContactMappingProfile()
        {
            CreateMap<Contact, ContactGetDTO>()
                .ForMember(dest => dest.ReceptionSchedule, opt => opt.MapFrom<GenericResolver<Contact, ContactGetDTO>>())
                .ForMember(dest => dest.Notes, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Note
                        )
                    )
                )
                .ForMember(dest => dest.RegistrationTimes, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.RegistrationTime
                        )
                    )
                );

            CreateMap<ContactPostDTO, Contact>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
