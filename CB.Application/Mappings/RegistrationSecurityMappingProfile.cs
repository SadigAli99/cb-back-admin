

using CB.Application.DTOs.RegistrationSecurity;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class RegistrationSecurityMappingProfile : MappingProfile
    {
        public RegistrationSecurityMappingProfile() : base()
        {
            CreateMap<RegistrationSecurity, RegistrationSecurityGetDTO>()
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


            CreateMap<RegistrationSecurityCreateDTO, RegistrationSecurity>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

            CreateMap<RegistrationSecurityEditDTO, RegistrationSecurity>()
                .ForMember(dest => dest.Translations, src => src.Ignore());

        }
    }
}
