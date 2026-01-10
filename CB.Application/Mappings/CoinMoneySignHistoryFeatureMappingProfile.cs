
using CB.Application.DTOs.CoinMoneySignHistoryFeature;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class CoinMoneySignHistoryFeatureMappingProfile : MappingProfile
    {
        public CoinMoneySignHistoryFeatureMappingProfile()
        {
            CreateMap<MoneySignHistoryFeature, CoinMoneySignHistoryFeatureGetDTO>()
                .ForMember(dest => dest.MoneySignHistoryTitle, src => src.Ignore())
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<CoinMoneySignHistoryFeaturePostDTO, MoneySignHistoryFeature>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
