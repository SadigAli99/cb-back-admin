
using CB.Application.DTOs.StatisticalReportCategory;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class StatisticalReportCategoryMappingProfile : MappingProfile
    {
        public StatisticalReportCategoryMappingProfile() : base()
        {
            CreateMap<StatisticalReportCategory, StatisticalReportCategoryGetDTO>()
                    .ForMember(dest => dest.Titles, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.Title
                            )
                        )
                    );

            CreateMap<StatisticalReportCategoryCreateDTO, StatisticalReportCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());

            CreateMap<StatisticalReportCategoryEditDTO, StatisticalReportCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());
        }
    }
}
