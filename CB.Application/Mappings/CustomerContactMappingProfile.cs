
using CB.Application.DTOs.CustomerContact;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CustomerContactMappingProfile : MappingProfile
    {
        public CustomerContactMappingProfile() : base()
        {
            CreateMap<CustomerContact, CustomerContactGetDTO>()
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                })
                .ForMember(dest => dest.Descriptions, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Description
                    ));
                });


            CreateMap<CustomerContactCreateDTO, CustomerContact>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<CustomerContactEditDTO, CustomerContact>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
