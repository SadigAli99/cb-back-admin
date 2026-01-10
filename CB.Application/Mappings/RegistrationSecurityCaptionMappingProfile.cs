
using CB.Application.DTOs.RegistrationSecurityCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class RegistrationSecurityCaptionMappingProfile : MappingProfile
    {
        public RegistrationSecurityCaptionMappingProfile()
        {
            CreateMap<RegistrationSecurityCaption, RegistrationSecurityCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<RegistrationSecurityCaptionPostDTO, RegistrationSecurityCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
