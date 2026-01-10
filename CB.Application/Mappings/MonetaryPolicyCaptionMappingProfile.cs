
using CB.Application.DTOs.MonetaryPolicyCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class MonetaryPolicyCaptionMappingProfile : MappingProfile
    {
        public MonetaryPolicyCaptionMappingProfile()
        {
            CreateMap<MonetaryPolicyCaption, MonetaryPolicyCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<MonetaryPolicyCaptionPostDTO, MonetaryPolicyCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
