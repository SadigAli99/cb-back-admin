
using CB.Application.DTOs.CurrencyExchangeCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CurrencyExchangeCaptionMappingProfile : MappingProfile
    {
        public CurrencyExchangeCaptionMappingProfile()
        {
            CreateMap<CurrencyExchangeCaption, CurrencyExchangeCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<CurrencyExchangeCaptionPostDTO, CurrencyExchangeCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
