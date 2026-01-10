

using CB.Application.DTOs.RoadmapOverview;
using CB.Application.Mappings.Resolvers;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class RoadmapOverviewMappingProfile : MappingProfile
    {
        public RoadmapOverviewMappingProfile() : base()
        {
            CreateMap<RoadmapOverview, RoadmapOverviewGetDTO>()
                .ForMember(dest => dest.File, opt => opt.MapFrom<GenericResolver<RoadmapOverview, RoadmapOverviewGetDTO>>())
                .ForMember(dest => dest.Titles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.Title
                    ));
                })
                .ForMember(dest => dest.CoverTitles, opt =>
                {
                    opt.MapFrom(src => src.Translations.ToDictionary(
                        t => t.Language.Code,
                        t => t.CoverTitle
                    ));
                });


            CreateMap<RoadmapOverviewCreateDTO, RoadmapOverview>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

            CreateMap<RoadmapOverviewEditDTO, RoadmapOverview>()
                .ForMember(dest => dest.Translations, src => src.Ignore())
                .ForMember(dest => dest.FileType, src => src.Ignore())
                .ForMember(dest => dest.File, src => src.Ignore());

        }
    }
}
