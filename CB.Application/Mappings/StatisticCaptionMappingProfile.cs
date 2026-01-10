
using CB.Application.DTOs.StatisticCaption;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class StatisticCaptionMappingProfile : MappingProfile
    {
        public StatisticCaptionMappingProfile()
        {
            CreateMap<StatisticCaption, StatisticCaptionGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<StatisticCaptionPostDTO, StatisticCaption>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
