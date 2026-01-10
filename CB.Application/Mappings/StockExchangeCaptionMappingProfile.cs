
using CB.Application.DTOs.StockExchangeCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class StockExchangeCaptionMappingProfile : MappingProfile
    {
        public StockExchangeCaptionMappingProfile()
        {
            CreateMap<StockExchangeCaption, StockExchangeCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<StockExchangeCaptionPostDTO, StockExchangeCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
