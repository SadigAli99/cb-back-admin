
using CB.Application.DTOs.ClearingSettlementSystemCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class ClearingSettlementSystemCaptionMappingProfile : MappingProfile
    {
        public ClearingSettlementSystemCaptionMappingProfile()
        {
            CreateMap<ClearingSettlementSystemCaption, ClearingSettlementSystemCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<ClearingSettlementSystemCaptionPostDTO, ClearingSettlementSystemCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
