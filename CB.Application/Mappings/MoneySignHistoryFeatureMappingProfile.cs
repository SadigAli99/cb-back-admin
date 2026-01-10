
using CB.Application.DTOs.MoneySignHistoryFeature;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class MoneySignHistoryFeatureMappingProfile : MappingProfile
    {
        public MoneySignHistoryFeatureMappingProfile()
        {
            CreateMap<MoneySignHistoryFeature, MoneySignHistoryFeatureGetDTO>()
                .ForMember(dest => dest.MoneySignHistoryTitle, src => src.Ignore())
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<MoneySignHistoryFeaturePostDTO, MoneySignHistoryFeature>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
