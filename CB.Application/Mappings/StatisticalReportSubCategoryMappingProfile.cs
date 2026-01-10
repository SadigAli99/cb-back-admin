
using CB.Application.DTOs.StatisticalReportSubCategory;
using CB.Core.Entities;

namespace CB.Application.Mappings
{
    public class StatisticalReportSubCategoryMappingProfile : MappingProfile
    {
        public StatisticalReportSubCategoryMappingProfile() : base()
        {
            CreateMap<StatisticalReportSubCategory, StatisticalReportSubCategoryGetDTO>()
                    .ForMember(dest => dest.StatisticalReportCategory, src => src.Ignore())
                    .ForMember(dest => dest.Titles, opt =>
                        opt.MapFrom(src => src.Translations
                            .ToDictionary(
                                t => t.Language.Code,
                                t => t.Title
                            )
                        )
                    );

            CreateMap<StatisticalReportSubCategoryCreateDTO, StatisticalReportSubCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());

            CreateMap<StatisticalReportSubCategoryEditDTO, StatisticalReportSubCategory>()
                .ForMember(dest => dest.Translations, opt => opt.Ignore());
        }
    }
}
