
using CB.Application.DTOs.CapitalMarketRightCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CapitalMarketRightCaptionMappingProfile : MappingProfile
    {
        public CapitalMarketRightCaptionMappingProfile()
        {
            CreateMap<CapitalMarketRightCaption, CapitalMarketRightCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<CapitalMarketRightCaptionPostDTO, CapitalMarketRightCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
