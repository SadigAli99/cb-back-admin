
using CB.Application.DTOs.RoadmapSustainableFinance;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class RoadmapSustainableFinanceMappingProfile : MappingProfile
    {
        public RoadmapSustainableFinanceMappingProfile()
        {
            CreateMap<RoadmapSustainableFinance, RoadmapSustainableFinanceGetDTO>()
                .ForMember(dest => dest.Descriptions, opt =>
                    opt.MapFrom(src => src.Translations
                        .ToDictionary(
                            t => t.Language.Code,
                            t => t.Description
                        )
                    )
                );

            CreateMap<RoadmapSustainableFinancePostDTO, RoadmapSustainableFinance>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
