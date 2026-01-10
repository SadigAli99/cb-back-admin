
using CB.Application.DTOs.SecurityType;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class SecurityTypeMappingProfile : MappingProfile
    {
        public SecurityTypeMappingProfile() : base()
        {
            CreateMap<SecurityType, SecurityTypeGetDTO>()
                    .ForMember(dest => dest.Titles, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.Title
                            )
                        )
                    );

            CreateMap<SecurityTypeCreateDTO, SecurityType>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());

            CreateMap<SecurityTypeEditDTO, SecurityType>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());
        }
    }
}
