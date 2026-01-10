
using CB.Application.DTOs.OutOfCoinMoneySignHistoryFeature;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class OutOfCoinMoneySignHistoryFeatureMappingProfile : MappingProfile
    {
        public OutOfCoinMoneySignHistoryFeatureMappingProfile()
        {
            CreateMap<MoneySignHistoryFeature, OutOfCoinMoneySignHistoryFeatureGetDTO>()
                .ForMember(dest => dest.MoneySignHistoryTitle, src => src.Ignore())
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<OutOfCoinMoneySignHistoryFeaturePostDTO, MoneySignHistoryFeature>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
