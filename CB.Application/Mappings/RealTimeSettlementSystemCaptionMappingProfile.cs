
using CB.Application.DTOs.RealTimeSettlementSystemCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class RealTimeSettlementSystemCaptionMappingProfile : MappingProfile
    {
        public RealTimeSettlementSystemCaptionMappingProfile()
        {
            CreateMap<RealTimeSettlementSystemCaption, RealTimeSettlementSystemCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<RealTimeSettlementSystemCaptionPostDTO, RealTimeSettlementSystemCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
