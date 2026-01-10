
using CB.Application.DTOs.MacroprudentialPolicyFrameworkCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class MacroprudentialPolicyFrameworkCaptionMappingProfile : MappingProfile
    {
        public MacroprudentialPolicyFrameworkCaptionMappingProfile()
        {
            CreateMap<MacroprudentialPolicyFrameworkCaption, MacroprudentialPolicyFrameworkCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<MacroprudentialPolicyFrameworkCaptionPostDTO, MacroprudentialPolicyFrameworkCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
